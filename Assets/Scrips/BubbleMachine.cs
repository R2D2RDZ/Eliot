using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class BubbleMachine : MonoBehaviour { 

    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispararán las balas
    public float bulletSpeed = 5f; // Velocidad del proyectil
    public float randomYmin = 0f;
    public float randomYmax = 360f;
    public float randomXmin = 45f;
    public float randomXmax = 135f;
    public int delaycount = 5;
    public float bulletlife = 2;

    private int count = 0;
    void FixedUpdate()
    {
        float randomY = Random.Range(randomYmin, randomYmax);
        float randomX = Random.Range(randomXmin, randomXmax);
        transform.rotation = Quaternion.Euler(randomX, randomY, transform.rotation.eulerAngles.z);
        Shoot();
    }
    void Shoot()
    {
        if (count == delaycount)
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
            Object.Destroy(bullet, bulletlife);
            count = 0;
        }
        else
        {
            count++;
        }
    }
}

