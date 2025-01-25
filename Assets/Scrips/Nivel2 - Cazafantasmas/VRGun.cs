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

    [Header("Controladores de las manos")]
    public Transform leftHandController; // Referencia al controlador de la mano izquierda
    public Transform rightHandController; // Referencia al Transform del RightHand Controller

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
                    Shoot();
                }
            }
            else if (currentHandController == rightHandController)
            {
                if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightTriggerValue) && rightTriggerValue)
                {
                    Shoot();
                }
            }
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


/*public class VRGun : MonoBehaviour
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
}*/

/*public class VRGun : MonoBehaviour
{
    [Header("Configuración del arma")]
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispararán las balas
    public float bulletSpeed = 5f; // Velocidad del proyectil
    public float grabDistance = 0.5f; // Distancia máxima para agarrar el arma

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

        // Detectar si la mano izquierda o derecha está agarrando el arma
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Detectar si los botones de grip están presionados
        leftHand.TryGetFeatureValue(CommonUsages.gripButton, out bool leftGripValue);
        rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool rightGripValue);

        // Comprobar si las manos están cerca del arma
        bool leftHandNear = IsHandNear(leftHandController);
        bool rightHandNear = IsHandNear(rightHandController);

        // Determinar qué mano está agarrando el arma
        if (leftGripValue && leftHandNear)
        {
            isGripped = true;
            currentHandController = leftHandController;
        }
        else if (rightGripValue && rightHandNear)
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
        if (isGripped && currentHandController != null && (leftGripValue || rightGripValue))
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
            rb.useGravity = false; // Hacer que la bala flote
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // Destruye la bala después de 2 segundos
        Destroy(bullet, 2f);
    }

    private bool IsHandNear(Transform hand)
    {
        // Comprobar si la distancia entre la mano y el arma está dentro del rango
        return Vector3.Distance(transform.position, hand.position) <= grabDistance;
    }
}*/

/*public class VRGun : MonoBehaviour
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
    private bool ghostsSpawned = false; // Controla si los fantasmas ya han sido generados

    void Start()
    {
        // Encuentra los controladores automáticamente si no están asignados
        var xrRig = GameObject.FindFirstObjectByType<XROrigin>();

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
        if (rightHandController == null) return;

        // Detectar si la mano izquierda o derecha está agarrando el arma
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Detectar si los botones de grip están presionados
        leftHand.TryGetFeatureValue(CommonUsages.gripButton, out bool leftGripValue);
        rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool rightGripValue);

        // Comprobar si las manos están cerca del arma
        bool leftHandNear = IsHandNear(leftHandController);
        bool rightHandNear = IsHandNear(rightHandController);

        // Determinar qué mano está agarrando el arma
        if (leftGripValue && leftHandNear)
        {
            isGripped = true;
            currentHandController = leftHandController;
        }
        else if (rightGripValue && rightHandNear)
        {
            isGripped = true;
            currentHandController = rightHandController;
        }
        else
        {
            isGripped = false;
            currentHandController = null;
        }

        // Si el arma está agarrada y los fantasmas aún no se han generado
        if (isGripped && currentHandController != null && !ghostsSpawned)
        {
            Object.FindAnyObjectByType<Ghost>().SpawnGhosts(); // Generar fantasmas
            ghostsSpawned = true; // Marcar como generados
        }

        // Si el arma está agarrada, sigue la posición y rotación de la mano correspondiente
        if (isGripped && currentHandController != null)
        {
            transform.position = currentHandController.position;
            transform.rotation = currentHandController.rotation;
        }

        // Si el trigger está presionado mientras el arma está agarrada, dispara
        if (isGripped && currentHandController != null && (leftGripValue || rightGripValue))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; // Las balas flotan
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        Destroy(bullet, 2f);
    }

    private bool IsHandNear(Transform hand)
    {
        return Vector3.Distance(transform.position, hand.position) <= 0.5f;
    }
}*/


