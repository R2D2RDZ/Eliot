using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad del fantasma
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
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Muebles")) // Si golpea una pared
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
        Destroy(gameObject, 2f); // Destruir el fantasma después de 2 segundos
    }
}