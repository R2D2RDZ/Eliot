using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement; // Para cambiar de escena

public class VRGun : MonoBehaviour
{
    [Header("Configuración del arma")]
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispararán las balas
    public float bulletSpeed = 5f; // Velocidad del proyectil
    public float grabDistance = 0.5f; // Distancia máxima para agarrar el arma
    public float shootDelay = 0.5f; // Tiempo entre disparos

    [Header("Efecto de sonido")]
    public AudioClip shootSound; // Efecto de sonido al disparar
    public AudioSource audioSource; // Componente AudioSource para reproducir el sonido

    [Header("Controladores de las manos")]
    public Transform leftHandController; // Referencia al controlador de la mano izquierda
    public Transform rightHandController; // Referencia al Transform del RightHand Controller

    [Header("Contador de Fantasmas")]
    public int ghostCount = 0; // Número de fantasmas restantes
    public Ghost ghostManager; // Referencia al script Ghost para obtener el conteo inicial

    private Transform currentHandController; // La mano actual que está agarrando el arma
    private bool isGripped = false; // Estado para saber si el arma está agarrada
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

        // Inicializar el contador de fantasmas
        if (ghostManager != null)
        {
            ghostCount = ghostManager.spawnPoints.Length; // Número de puntos de spawn = número inicial de fantasmas
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

        // Si el arma está agarrada, sigue la posición y rotación de la mano correspondiente
        if (isGripped && currentHandController != null)
        {
            transform.position = currentHandController.position;
            transform.rotation = currentHandController.rotation;

            // Invocar fantasmas desde el GhostController
            Object.FindAnyObjectByType<Ghost>().SpawnGhosts();
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
            lastShootTime = Time.time; // Actualiza el tiempo del último disparo
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

        // Reproduce el sonido de disparo
        PlayShootSound();

        // Destruye la bala después de 2 segundos
        Destroy(bullet, 1f);
    }

    void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound); // Reproduce el sonido de disparo
        }
    }

    public void DecreaseGhostCount()
    {
        ghostCount--;

        // Verificar si todos los fantasmas han sido destruidos
        if (ghostCount <= 0)
        {
            ChangeScene(); // Cambiar a la escena "Pasillo"
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Pasillo"); // Cambiar a la escena llamada "Pasillo"
    }
}