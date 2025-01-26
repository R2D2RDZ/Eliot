using UnityEngine;

/*public class SlowGhost : GhostMovement
{
    private float angle = 0f; // Ángulo para el movimiento circular

    protected override void Move()
    {
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * roomBounds.x / 4f;
        float z = Mathf.Sin(angle) * roomBounds.z / 4f;
        transform.position = new Vector3(x, transform.position.y, z);
    }
}*/
public class GhostMovement : MonoBehaviour
{
    public float speed; // Velocidad del fantasma
    public Vector3 roomBounds = new Vector3(24f, 8f, 24f); // Límites de la habitación (x, y, z)
    public float floatSpeed = 1.5f; // Velocidad del efecto de "flotación"
    public float changeDirectionInterval = 2f; // Intervalo de tiempo para cambiar de dirección
    public string furnitureTag; // Tag de los muebles
    public string bulletTag; // Tag de las balas

    private Vector3 targetPosition; // La posición a la que el fantasma se moverá
    private bool isStopped = false; // Estado para saber si el fantasma está detenido

    void Start()
    {
        SetRandomTargetPosition(); // Inicializa una posición aleatoria como objetivo
        InvokeRepeating(nameof(SetRandomTargetPosition), changeDirectionInterval, changeDirectionInterval); // Cambia de dirección periódicamente
    }

    void Update()
    {
        if (!isStopped)
        {
            MoveTowardsTarget(); // Mueve al fantasma
            FloatEffect(); // Aplica el efecto de flotación
        }
    }

    private void MoveTowardsTarget()
    {
        // Esquivar muebles con raycast
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1f))
        {
            if (hit.collider.CompareTag(furnitureTag))
            {
                targetPosition = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            }
        }

        // Mover hacia el objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si llega al objetivo, calcula un nuevo objetivo
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private void FloatEffect()
    {
        // Movimiento vertical para el efecto de flotación
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
        if (collision.gameObject.CompareTag(bulletTag)) // Detener al ser impactado por una bala
        {
            StopGhost();
        }
    }

    private void StopGhost()
    {
        isStopped = true; // Detener el movimiento
        speed = 0f;
        Debug.Log($"{gameObject.name} ha sido detenido.");
    }
}

