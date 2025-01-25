using UnityEngine;
using UnityEngine.XR;

/*public class VRGun : MonoBehaviour
{
    [Header("Configuraci�n del arma")]
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se disparar�
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
        // Si se presiona el bot�n de grip, el arma sigue la posici�n y rotaci�n de la mano
        if (OVRInput.Get(gripButton))
        {
            isGripped = true;
            // Mover el arma a la posici�n y rotaci�n de la mano
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
        // Si el arma est� en grip y el bot�n de disparo est� presionado 
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
}*/

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
}
