using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class VRGun : MonoBehaviour
{
    [Header("Configuración del arma")]
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispararán las balas
    public float bulletSpeed = 5f; // Velocidad del proyectil

    [Header("Controladores de las manos")]
    public Transform leftHandController; // Referencia al controlador de la mano izquierda
    private Transform rightHandController; // Referencia al Transform del RightHand Controller

    private Transform currentHandController; // La mano actual que está agarrando el arma
    private bool isGripped = false; // Estado para saber si el arma está agarrada

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
        if (rightHandController == null) return; // Si no se encuentra el controlador, no hacer nada

        //Detectar si la mano izquierda o derecha esta agarrando el arma
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Detectar si los botones de grip estan presionados
        leftHand.TryGetFeatureValue(CommonUsages.gripButton, out bool leftGripValue);
        rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool rightGripValue);

        // Detectar si los botones de trigger estan presionados
        leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftTriggerValue);
        rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightTriggerValue);

        // Determinar qué mano está agarrando el arma
        if (leftGripValue)
        {
            isGripped = true;
            currentHandController = leftHandController;
        }
        else if (rightGripValue)
        {
            isGripped = true;
            currentHandController = rightHandController;
        }
        else
        {
            isGripped = false;
            currentHandController = null;
        }

        // Si el arma está agarrada, sigue la posición y rotación de la mano correspondiente
        if (isGripped && currentHandController != null)
        {
            transform.position = currentHandController.position;
            transform.rotation = currentHandController.rotation;

            // Invocar fantasmas desde el GhostController
            Object.FindAnyObjectByType<Ghost>().SpawnGhosts();
        }

        // Si el trigger está presionado mientras el arma está agarrada, dispara
        if (isGripped && currentHandController != null && (leftTriggerValue || rightTriggerValue))
        {
            Shoot();
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
}
