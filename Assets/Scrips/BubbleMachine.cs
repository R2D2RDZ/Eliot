using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class BubbleMachine : MonoBehaviour { 

    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispararán las balas
    public float bulletSpeed = 5f; // Velocidad del proyectil

    private int count = 0;
    void FixedUpdate()
    {
        float randomY = Random.Range(0f, 360f);
        float randomX = Random.Range(45f, 135f);
        transform.rotation = Quaternion.Euler(randomX, randomY, transform.rotation.eulerAngles.z);
        Shoot();
    }
    void Shoot()
    {
        if (count == 5)
        {
            // Instancia una bala en el FirePoint
            GameObject bullet = Object.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Aplica velocidad al Rigidbody de la bala
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * bulletSpeed;
            }

            // Destruye la bala después de 2 segundos
            Object.Destroy(bullet, 2f);
            count = 0;
        }
        else
        {
            count++;
        }
    }
}

