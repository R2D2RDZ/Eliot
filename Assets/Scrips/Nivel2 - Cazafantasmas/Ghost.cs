using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Configuración de los fantasmas")]
    public GameObject ghostPrefab; // Prefab del fantasma
    public Transform[] spawnPoints; // Puntos donde aparecerán los fantasmas
    public float movementSpeed = 2f; // Velocidad de movimiento de los fantasmas

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

/*public class GhostMovement : MonoBehaviour
{
    public float speed; // Velocidad del fantasma
    private bool isStopped = false; // Estado para saber si el fantasma está detenido

    void Update()
    {
        if (!isStopped)
        {
            // Movimiento simple hacia adelante
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisionó tiene el tag "BurbujaBala"
        if (collision.gameObject.CompareTag("BurbujaBala"))
        {
            StopGhost(); // Detener al fantasma
        }
    }

    private void StopGhost()
    {
        isStopped = true; // Cambiar el estado para detener el movimiento
        speed = 0f; // Asegurarse de que la velocidad sea 0
        Debug.Log($"{gameObject.name} ha sido detenido.");
    }
}*/

public class GhostMovement : MonoBehaviour
{
    public float speed; // Velocidad del fantasma
    public Vector3 roomBounds = new Vector3(24f, 8f, 24f); // Límites de la habitación (x, y, z)
    public float floatSpeed = 0.5f; // Velocidad del efecto de "flotación"
    public float changeDirectionInterval = 2f; // Intervalo de tiempo para cambiar de dirección

    private Vector3 targetPosition; // La posición a la que el fantasma se moverá
    private bool isStopped = false; // Estado para saber si el fantasma está detenido

    void Start()
    {
        // Inicializa una posición aleatoria como objetivo
        SetRandomTargetPosition();

        // Cambia de dirección periódicamente
        InvokeRepeating(nameof(SetRandomTargetPosition), changeDirectionInterval, changeDirectionInterval);
    }

    void Update()
    {
        if (!isStopped)
        {
            // Mueve al fantasma hacia la posición objetivo
            MoveTowardsTarget();

            // Efecto de flotación vertical
            FloatEffect();
        }
    }

    private void MoveTowardsTarget()
    {
        // Mueve el fantasma hacia la posición objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si llega al objetivo, calcula un nuevo objetivo aleatorio
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private void FloatEffect()
    {
        // Pequeño movimiento vertical (efecto de flotación)
        transform.position += new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * 0.01f, 0);
    }

    private void SetRandomTargetPosition()
    {
        // Genera una posición aleatoria dentro de los límites de la habitación
        targetPosition = new Vector3(
            Random.Range(-roomBounds.x, roomBounds.x),
            Random.Range(0f, roomBounds.y), // Asegúrate de que se mantenga dentro de los límites verticales
            Random.Range(-roomBounds.z, roomBounds.z)
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // Si golpea una pared
        {
            targetPosition = -targetPosition; // Cambia la dirección
        }

        // Verifica si el objeto que colisionó tiene el tag "BurbujaBala"
        if (collision.gameObject.CompareTag("BurbujaBala"))
        {
            StopGhost(); // Detener al fantasma
        }
    }

    private void StopGhost()
    {
        isStopped = true; // Cambiar el estado para detener el movimiento
        speed = 0f; // Asegurarse de que la velocidad sea 0
        Debug.Log($"{gameObject.name} ha sido detenido.");
        Destroy(gameObject, 2f); // Destruir el fantasma después de 2 segundos
    }
}

