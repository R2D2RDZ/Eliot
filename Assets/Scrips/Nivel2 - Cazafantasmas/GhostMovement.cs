using UnityEngine;

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
        //Destroy(gameObject, 2f); // Destruir el fantasma después de 2 segundos
    }
}

/*public class SlowGhostMovement : MonoBehaviour
{
    public float speed = 1f; // Velocidad lenta del fantasma
    public float circleRadius = 2f; // Radio del círculo
    public float circleSpeed = 1f; // Velocidad de rotación alrededor del círculo
    public Vector3 centerPoint; // Centro del movimiento circular

    private float angle; // Ángulo actual del movimiento circular
    private bool isStopped = false; // Estado para saber si el fantasma está detenido

    void Start()
    {
        // Establece el centro inicial como la posición del fantasma
        centerPoint = transform.position;
    }

    void Update()
    {
        MoveInCircle();
    }

    private void MoveInCircle()
    {
        // Incrementa el ángulo basado en la velocidad
        angle += circleSpeed * Time.deltaTime;

        // Calcula la posición en el círculo
        float x = Mathf.Cos(angle) * circleRadius;
        float z = Mathf.Sin(angle) * circleRadius;

        // Actualiza la posición del fantasma
        transform.position = Vector3.MoveTowards(transform.position, centerPoint + new Vector3(x, 0, z), speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BurbujaBala"))
        {
            StopGhost(); // Detener al fantasma
        }
    }

    private void StopGhost()
    {
        isStopped = true; // Cambiar el estado para detener el movimiento
        speed = 0f; // Asegurarse de que la velocidad sea 0
    }
}*/

