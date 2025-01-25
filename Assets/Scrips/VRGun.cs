using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

/*public class VRGun : MonoBehaviour
{
    [Header("Configuracion del arma")]
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se disparara
    public float bulletSpeed = 20f; // Velocidad del proyectil
    public float fireRate = 0.5f; // Tiempo entre disparos

    private float nextFireTime = 0f; // Tiempo para el siguiente disparo

    [Header("Controlador VR")]
    public Transform handTransform; // Transform del controlador (mano)
    public OVRInput.Button gripButton = OVRInput.Button.PrimaryHandTrigger; // Bot�n de grip
    public OVRInput.Button fireButton = OVRInput.Button.PrimaryIndexTrigger; // Bot�n de disparo

    private bool isGripped = false; // Indica si el arma est� en grip

    void Update()
    {
        HandleGrip();
        HandleShooting();
    }

    void HandleGrip()
    {
        // Si se presiona el boton de grip, el arma sigue la posicion y rotacion de la mano
        if (OVRInput.Get(gripButton))
        {
            isGripped = true;
            // Mover el arma a la posicion y rotacion de la mano
            transform.position = handTransform.position;
            transform.rotation = handTransform.rotation;
        }
        else
        {
            isGripped = false;
        }
    }

    void HandleShooting()
    {
        // Si el arma esta en grip y el boton de disparo esta presionado 
        if (isGripped && OVRInput.GetDown(fireButton) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Crea el proyectil
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Aplica fuerza al proyectil para que se mueva
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // Destruye el proyectil despu�s de 5 segundos
        Destroy(bullet, 5f);
    }
}

public class VRGun : MonoBehaviour
{
    [Header("Configuracion del arma")]
    public GameObject bulletPrefab; // Prefab del proyectil que sera disparado
    public Transform firePoint; // Punto desde donde se dispararon las balas
    public float bulletSpeed = 20f; // Velocidad del proyectil al disparar

    [Header("Controlador de la mano")]
    public Transform handTransform; // Transform de la mano o controlador que sostendra el arma

    private bool isGripped = false; // Estado para saber si el arma esta "agarrada"

    void Update()
    {
        // Obten el controlador de la mano derecha
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Verifica si se esta presionando el boton de "grip" (para agarrar el arma)
        rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue);

        // Verifica si se esta presionando el boton de "trigger" (para disparar)
        rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue);

        // Si se presiona el boton de grip, el arma sigue la posicion y rotacion de la mano
        if (gripValue)
        {
            isGripped = true;
            transform.position = handTransform.position;
            transform.rotation = handTransform.rotation;
        }
        else
        {
            isGripped = false; // Si se suelta el grip, el arma deja de seguir la mano
        }

        // Si el arma esta agarrada y se presiona el trigger, dispara
        if (isGripped && triggerValue)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Crea una bala en el punto de disparo
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Aplica una velocidad al Rigidbody de la bala en la direccion del FirePoint
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // Destruye la bala automaticamente despues de 5 segundos para evitar saturar la escena
        Destroy(bullet, 5f);
    }
}*/

public class VRGun : MonoBehaviour
{
    [Header("Configuración del arma")]
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispararán las balas
    public float bulletSpeed = 20f; // Velocidad del proyectil

    private Transform rightHandController; // Referencia al Transform del RightHand Controller
    private bool isGripped = false; // Estado para saber si el arma está agarrada

    void Start()
    {
        // Busca el controlador derecho dentro del XR Rig
        var xrRig = GameObject.FindAnyObjectByType<XROrigin>(); // Encuentra el XR Origin
        if (xrRig != null)
        {
            rightHandController = xrRig.transform.Find("Camera Offset/Right Controller");
        }

        if (rightHandController == null)
        {
            Debug.LogError("Right Controller no encontrado. Asegúrate de que está configurado en el XR Rig.");
        }
    }

    void Update()
    {
        if (rightHandController == null) return; // Si no se encuentra el controlador, no hacer nada

        // Detecta los inputs del controlador derecho
        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Verifica si el botón de grip está presionado
        rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue);

        // Verifica si el botón de trigger (disparo) está presionado
        rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue);

        // Si el botón de grip está presionado, el arma sigue la mano
        if (gripValue)
        {
            isGripped = true;
            transform.position = rightHandController.position; // Sigue la posición del controlador
            transform.rotation = rightHandController.rotation; // Sigue la rotación del controlador
        }
        else
        {
            isGripped = false; // Si el grip se suelta, el arma deja de seguir la mano
        }

        // Si el arma está agarrada y el trigger está presionado, dispara
        if (isGripped && triggerValue)
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

        // Destruye la bala después de 5 segundos
        Destroy(bullet, 5f);
    }
}
