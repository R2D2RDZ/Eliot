using UnityEngine;
/*public class GhostMovement : MonoBehaviour
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
}*/

public class SlowGhostMovement : MonoBehaviour
{
    public float speed = 1f; // Velocidad lenta del fantasma
    public float circleRadius = 2f; // Radio del círculo
    public float circleSpeed = 1f; // Velocidad de rotación alrededor del círculo
    public Vector3 centerPoint; // Centro del movimiento circular

    private float angle; // Ángulo actual del movimiento circular

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
}

public class ModerateGhostMovement : MonoBehaviour
{
    public float speed = 3f; // Velocidad moderada del fantasma
    public float zigzagAmplitude = 2f; // Amplitud del zigzag
    public float zigzagFrequency = 2f; // Frecuencia del zigzag

    private Vector3 direction; // Dirección principal del movimiento
    private float time; // Contador de tiempo para el zigzag

    void Start()
    {
        // Dirección inicial aleatoria
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        MoveInZigzag();
    }

    private void MoveInZigzag()
    {
        // Calcula el desplazamiento en zigzag
        float offset = Mathf.Sin(time * zigzagFrequency) * zigzagAmplitude;

        // Dirección del zigzag
        Vector3 zigzagOffset = new Vector3(direction.z, 0, -direction.x) * offset;

        // Mueve al fantasma en la dirección principal con el zigzag añadido
        transform.position += (direction + zigzagOffset).normalized * speed * Time.deltaTime;

        // Incrementa el tiempo para el zigzag
        time += Time.deltaTime;
    }
}

public class FastGhostMovement : MonoBehaviour
{
    public float speed = 6f; // Velocidad rápida del fantasma
    public float circleRadius = 3f; // Radio del movimiento circular
    public float circleSpeed = 2f; // Velocidad del movimiento circular
    public float zigzagAmplitude = 2f; // Amplitud del zigzag
    public float zigzagFrequency = 3f; // Frecuencia del zigzag
    public Transform player; // Referencia al jugador

    private float angle; // Ángulo para el movimiento circular
    private float time; // Contador de tiempo para el zigzag
    private bool hidingBehindPlayer = false; // Estado de esconderse

    void Update()
    {
        if (!hidingBehindPlayer)
        {
            // Decide el tipo de movimiento basado en tiempo
            if (Time.time % 5 < 2.5f)
                MoveInCircle();
            else
                MoveInZigzag();
        }

        if (ShouldHide())
            HideBehindPlayer();
    }

    private void MoveInCircle()
    {
        angle += circleSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * circleRadius;
        float z = Mathf.Sin(angle) * circleRadius;
        transform.position += new Vector3(x, 0, z) * speed * Time.deltaTime;
    }

    private void MoveInZigzag()
    {
        float offset = Mathf.Sin(time * zigzagFrequency) * zigzagAmplitude;
        Vector3 direction = transform.forward + new Vector3(offset, 0, 0);
        transform.position += direction.normalized * speed * Time.deltaTime;
        time += Time.deltaTime;
    }

    private bool ShouldHide()
    {
        // Determina si debe esconderse (por ejemplo, si está cerca del jugador)
        return Vector3.Distance(transform.position, player.position) < 10f;
    }

    private void HideBehindPlayer()
    {
        hidingBehindPlayer = true;
        Vector3 hidePosition = player.position - player.forward * 2f; // Posición detrás del jugador
        transform.position = Vector3.MoveTowards(transform.position, hidePosition, speed * Time.deltaTime);
        hidingBehindPlayer = false;
    }
}

