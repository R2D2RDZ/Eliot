using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class VRGun : MonoBehaviour
{
    [Header("Configuración del arma")]
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispararán las balas
    public float bulletSpeed = 5f; // Velocidad del proyectil
    public float grabDistance = 0.5f; // Distancia máxima para agarrar el arma
    public float shootDelay = 0.5f; // Tiempo entre disparos
    public float maxDistanceFromHand = 150f; // Distancia máxima permitida antes de "resetear" el arma

    [Header("Controladores de las manos")]
    public Transform leftHandController; // Referencia al controlador de la mano izquierda
    public Transform rightHandController; // Referencia al Transform del RightHand Controller

    private Transform currentHandController; // La mano actual que está agarrando el arma
    public bool isGripped = false; // Estado para saber si el arma está agarrada
    public bool isShooting = false;
    private float lastShootTime = 0f; // Tiempo del último disparo

    void Start()
    {
        // Encuentra los controladores automáticamente si no están asignados
        var xrRig = GameObject.FindFirstObjectByType<XROrigin>(); // Encuentra el XR Origin

        if (leftHandController == null || rightHandController == null)
        {
            if (xrRig != null)
            {
                leftHandController = xrRig.transform.Find("Camera Offset/Left Controller");
                rightHandController = xrRig.transform.Find("Camera Offset/Right Controller");
            }

            if (rightHandController == null)
            {
                Debug.LogError("Right Controller no encontrado. Asegúrate de que está configurado en el XR Rig.");
            }
            if (leftHandController == null)
            {
                Debug.LogError("Left Controller no encontrado. Asegúrate de que está configurado en el XR Rig.");
            }
        }
    }

    void Update()
    {
        if (rightHandController == null || leftHandController == null) return; // Si no se encuentran los controladores, no hacer nada

        // Detectar si la mano izquierda o derecha está agarrando el arma
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Detectar si los botones de grip están presionados
        leftHand.TryGetFeatureValue(CommonUsages.gripButton, out bool leftGripValue);
        rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool rightGripValue);

        // Detectar la distancia entre el arma y los controladores
        float distanceToLeftHand = Vector3.Distance(transform.position, leftHandController.position);
        float distanceToRightHand = Vector3.Distance(transform.position, rightHandController.position);

        // Determinar qué mano está agarrando el arma
        if (leftGripValue && distanceToLeftHand <= grabDistance)
        {
            isGripped = true;
            currentHandController = leftHandController;
        }
        else if (rightGripValue && distanceToRightHand <= grabDistance)
        {
            isGripped = true;
            currentHandController = rightHandController;
        }
        else
        {
            isGripped = false;
            currentHandController = null;
        }

        if (Vector3.Distance(leftHandController.position, rightHandController.position) < 0.25f)
        {
            isGripped = true;
            currentHandController = rightHandController;
        }

        // Si el arma está agarrada, sigue la posición y rotación de la mano correspondiente
        if (isGripped && currentHandController != null)
        {
            transform.position = currentHandController.position;
            transform.rotation = currentHandController.rotation;

            // Invocar fantasmas desde el GhostController
            Object.FindAnyObjectByType<Ghost>().SpawnGhosts();

            // Validar si el arma se aleja demasiado de la mano
            if (Vector3.Distance(transform.position, currentHandController.position) > maxDistanceFromHand)
            {
                ResetPositionToHand();
            }
        }

        // Detectar disparo con el controlador que sostiene el arma
        if (isGripped && currentHandController != null)
        {
            if (currentHandController == leftHandController)
            {
                if (leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftTriggerValue) && leftTriggerValue)
                {
                    TryShoot();
                }
            }
            else if (currentHandController == rightHandController)
            {
                if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightTriggerValue) && rightTriggerValue)
                {
                    TryShoot();
                }
            }
        }
    }

    void TryShoot()
    {
        // Verifica si ha pasado suficiente tiempo desde el último disparo
        if (Time.time - lastShootTime >= shootDelay)
        {
            Shoot();
            isShooting = true;
            lastShootTime = Time.time; // Actualiza el tiempo del último disparo
        }
        else
        {
            isShooting = false;
        }
    }

    void Shoot()
    {
        // Instancia una bala en el FirePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Aplica velocidad al Rigidbody de la bala
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // Destruye la bala después de 2 segundos
        Destroy(bullet, 2f);
    }

    void ResetPositionToHand()
    {
        // Reposiciona el arma en la mano actual
        if (currentHandController != null)
        {
            transform.position = currentHandController.position;
            transform.rotation = currentHandController.rotation;
        }
    }
}