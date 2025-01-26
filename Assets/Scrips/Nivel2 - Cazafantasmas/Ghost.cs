using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Configuración de los fantasmas")]
    public GameObject ghostPrefab; // Prefab del fantasma
    public Transform[] spawnPoints; // Puntos donde aparecerán los fantasmas
    public float movementSpeed = 2.5f; // Velocidad de movimiento de los fantasmas

    private bool ghostsSpawned = false; // Verifica si los fantasmas ya aparecieron

    public void SpawnGhosts()
    {
        if (ghostsSpawned) return; // Evitar que se invoquen varias veces

        // Crear un fantasma en cada punto de aparición
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject ghost = Instantiate(ghostPrefab, spawnPoint.position, spawnPoint.rotation);

            // Configurar su movimiento
            GhostMovement movement = ghost.AddComponent<GhostMovement>();
            movement.speed = movementSpeed;

            // Asegurarse de que el fantasma tenga el tag "Ghost"
            ghost.tag = "Ghost";
        }

        ghostsSpawned = true; // Marcar que los fantasmas ya fueron generados
    }
}

/*public class Ghost : MonoBehaviour
{
    [Header("Configuración de los fantasmas")]
    public GameObject slowGhostPrefab; // Prefab del fantasma Slow
    public GameObject moderateGhostPrefab; // Prefab del fantasma Moderate
    public GameObject fastGhostPrefab; // Prefab del fantasma Fast
    public Transform[] spawnPoints; // Puntos donde aparecerán los fantasmas
    public float movementSpeed = 2.5f; // Velocidad base de movimiento para los fantasmas

    [Header("Probabilidades de aparición")]
    [Range(0, 100)] public int slowChance = 33; // Probabilidad (%) de que aparezca un Slow
    [Range(0, 100)] public int moderateChance = 33; // Probabilidad (%) de que aparezca un Moderate
    [Range(0, 100)] public int fastChance = 34; // Probabilidad (%) de que aparezca un Fast

    private bool ghostsSpawned = false; // Verifica si los fantasmas ya aparecieron

    public void SpawnGhosts()
    {
        if (ghostsSpawned) return; // Evitar que se invoquen varias veces

        // Crear un fantasma en cada punto de aparición
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject selectedGhostPrefab = ChooseGhostPrefab(); // Selecciona el prefab del fantasma
            GameObject ghost = Instantiate(selectedGhostPrefab, spawnPoint.position, spawnPoint.rotation);

            // Configurar su movimiento basado en el tipo de fantasma
            if (ghost.TryGetComponent(out SlowGhostMovement slowMovement))
            {
                slowMovement.speed = movementSpeed * 0.5f; // Ajusta velocidad para Slow
            }
            else if (ghost.TryGetComponent(out ModerateGhostMovement moderateMovement))
            {
                moderateMovement.speed = movementSpeed; // Ajusta velocidad para Moderate
            }
            else if (ghost.TryGetComponent(out FastGhostMovement fastMovement))
            {
                fastMovement.speed = movementSpeed * 1.5f; // Ajusta velocidad para Fast
            }

            // Asegurarse de que el fantasma tenga el tag "Ghost"
            ghost.tag = "Ghost";
        }

        ghostsSpawned = true; // Marcar que los fantasmas ya fueron generados
    }

    private GameObject ChooseGhostPrefab()
    {
        int randomValue = Random.Range(1, 101); // Genera un número entre 1 y 100

        if (randomValue <= slowChance)
        {
            return slowGhostPrefab; // Selecciona Slow
        }
        else if (randomValue <= slowChance + moderateChance)
        {
            return moderateGhostPrefab; // Selecciona Moderate
        }
        else
        {
            return fastGhostPrefab; // Selecciona Fast
        }
    }
}*/
