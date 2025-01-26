using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class BubbleMachine
{
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se disparar�n las balas
    public float bulletSpeed = 5f; // Velocidad del proyectil
    void FixedUpdate()
    {
        Shoot();
    }
    void Shoot()
    {
        // Instancia una bala en el FirePoint
        GameObject bullet = Object.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Aplica velocidad al Rigidbody de la bala
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // Destruye la bala despu�s de 2 segundos
        Object.Destroy(bullet, 2f);
    }
}
