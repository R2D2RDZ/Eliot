using UnityEngine;

/*public class Ghost : MonoBehaviour
{
    public GameObject ghostPrefab; // Prefab base de los fantasmas
    public int numberOfGhosts = 3; // Número de fantasmas a generar
    public Vector3 roomBounds = new Vector3(24f, 8f, 24f); // Límites de la habitación

    private bool ghostsSpawned = false; // Verificar si los fantasmas ya fueron generados

    public void SpawnGhosts()
    {
        if (ghostsSpawned) return;

        for (int i = 0; i < numberOfGhosts; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(ghostPrefab, randomPosition, Quaternion.identity);
        }

        ghostsSpawned = true; // Marcar como generados
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-roomBounds.x / 2f, roomBounds.x / 2f),
            Random.Range(0f, roomBounds.y),
            Random.Range(-roomBounds.z / 2f, roomBounds.z / 2f)
        );
    }
}*/


/*public class Ghost : MonoBehaviour
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

public class GhostMovement : MonoBehaviour
{
    public float speed; // Velocidad del fantasma
    public Vector3 roomBounds = new Vector3(24f, 8f, 24f); // Límites de la habitación (x, y, z)
    public float floatSpeed = 1.5f; // Velocidad del efecto de "flotación"
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
        //Destroy(gameObject, 2f); // Destruir el fantasma después de 2 segundos
    }
}*/


public class Ghost : MonoBehaviour
{
    public GameObject slowGhostPrefab;
    public GameObject moderateGhostPrefab;
    public GameObject fastGhostPrefab;

    public int numberOfEachType = 3; // Número de cada tipo de fantasma
    public Vector3 roomBounds = new Vector3(24f, 8f, 24f);

    private bool ghostsSpawned = false;

    public void SpawnGhosts()
    {
        if (ghostsSpawned) return;

        // Generar fantasmas lentos
        for (int i = 0; i < numberOfEachType; i++)
        {
            Instantiate(slowGhostPrefab, GetRandomPosition(), Quaternion.identity);
        }

        // Generar fantasmas moderados
        for (int i = 0; i < numberOfEachType; i++)
        {
            Instantiate(moderateGhostPrefab, GetRandomPosition(), Quaternion.identity);
        }

        // Generar fantasmas rápidos
        for (int i = 0; i < numberOfEachType; i++)
        {
            Instantiate(fastGhostPrefab, GetRandomPosition(), Quaternion.identity);
        }

        ghostsSpawned = true;
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-roomBounds.x / 2f, roomBounds.x / 2f),
            Random.Range(0f, roomBounds.y),
            Random.Range(-roomBounds.z / 2f, roomBounds.z / 2f)
        );
    }
}

public abstract class GhostMovement : MonoBehaviour
{
    public float speed; // Velocidad del fantasma
    public Vector3 roomBounds = new Vector3(24f, 8f, 24f); // Límites de la habitación
    protected Transform player; // Referencia al jugador
    protected bool isStopped = false; // Estado para detener al fantasma

    public virtual void Start()
    {
        // Encontrar al jugador por su tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Update()
    {
        if (!isStopped)
        {
            Move(); // Comportamiento de movimiento definido en cada tipo de fantasma
            HandleObstacles(); // Esquivar obstáculos
        }
    }

    protected abstract void Move(); // Cada fantasma define su propio movimiento

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BurbujaBala"))
        {
            StopGhost();
        }
    }

    private void StopGhost()
    {
        isStopped = true;
        speed = 0f;
        //Destroy(gameObject, 2f); // Destruir el fantasma después de 2 segundos
    }

    private void HandleObstacles()
    {
        // Esquivar obstáculos con el tag "Muebles"
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            if (hit.collider.CompareTag("Muebles"))
            {
                // Cambiar dirección ligeramente
                transform.Rotate(0, Random.Range(45f, 135f), 0);
            }
        }
    }
}
