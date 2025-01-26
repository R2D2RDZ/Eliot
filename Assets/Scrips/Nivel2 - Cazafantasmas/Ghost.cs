using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Configuración de los fantasmas")]
    public GameObject ghostPrefab; // Prefab del fantasma
    public Transform[] spawnPoints; // Puntos donde aparecerán los fantasmas
    public float movementSpeed = 2.5f; // Velocidad de movimiento de los fantasmas

    private bool ghostsSpawned = false; // Verifica si los fantasmas ya aparecieron
    private int ghostCount = 0; // Número de fantasmas activos
    private BackgroundMusicManager musicManager;

    private Pasillo sceneChanger; // Referencia al SceneChanger


    void Start()
    {
        musicManager = Object.FindAnyObjectByType<BackgroundMusicManager>();
        sceneChanger = FindAnyObjectByType<Pasillo>(); // Encuentra el controlador de escenas
    }
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

            // Configurar evento para reducir el conteo de fantasmas al destruirse
            ghost.GetComponent<GhostMovement>().OnDestroyed += DecreaseGhostCount;
            movement.OnDestroyed += sceneChanger.DecreaseGhostCount;

            // Asegurarse de que el fantasma tenga el tag "Ghost"
            ghost.tag = "Ghost";

            ghostCount++; // Incrementar el número de fantasmas
        }

        ghostsSpawned = true; // Marcar que los fantasmas ya fueron generados
        UpdateMusicManager(); // Actualizar el estado en el administrador de música
    }

    private void DecreaseGhostCount()
    {
        ghostCount--;
        UpdateMusicManager();
    }

    private void UpdateMusicManager()
    {
        if (musicManager != null)
        {
            musicManager.UpdateGhostCount(ghostCount);
        }
    }
}