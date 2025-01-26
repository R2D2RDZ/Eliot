using UnityEngine;

public class FastGhostSpawner : MonoBehaviour
{
    public GameObject fastGhostPrefab; // Prefab del fantasma "Fast"
    public Transform spawnPoint; // Punto de aparición
    public int ghostCount = 5; // Número de fantasmas a generar

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
            Instantiate(fastGhostPrefab, spawnPoint.position, Quaternion.identity);
        }

        isActivated = true;
    }
}
