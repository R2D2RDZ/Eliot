using UnityEngine;

public class SlowGhostSpawner : MonoBehaviour
{
    public GameObject slowGhostPrefab; // Prefab del fantasma "Slow"
    public Transform spawnPoint; // Punto de aparici�n
    public int ghostCount = 5; // N�mero de fantasmas a generar

    private bool isActivated = false;

    void OnEnable()
    {
        VRGun.OnWeaponGrabbed += ActivateSpawner; // Escuchar evento del arma
    }

    void OnDisable()
    {
        VRGun.OnWeaponGrabbed -= ActivateSpawner; // Dejar de escuchar evento
    }

    private void ActivateSpawner()
    {
        if (isActivated) return;

        for (int i = 0; i < ghostCount; i++)
        {
            Instantiate(slowGhostPrefab, spawnPoint.position, Quaternion.identity);
        }

        isActivated = true;
    }
}


