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
            //GameObject ghost = Instantiate(ghostPrefab, spawnPoint.position, spawnPoint.rotation);
            GameObject ghost = Instantiate(ghostPrefab, spawnPoint.position, Quaternion.Euler(-90, 0, 0));


            // Configurar su movimiento
            GhostMovement movement = ghost.AddComponent<GhostMovement>();
            movement.speed = movementSpeed;

            // Asegurarse de que el fantasma tenga el tag "Ghost"
            ghost.tag = "Ghost";
        }

        ghostsSpawned = true; // Marcar que los fantasmas ya fueron generados
    }
}