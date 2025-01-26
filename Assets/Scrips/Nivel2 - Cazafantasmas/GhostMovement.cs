using UnityEngine;
/*public class GhostMovement : MonoBehaviour
{
    public float speed; // Velocidad del fantasma
    public Vector3 roomBounds = new Vector3(24f, 8f, 24f); // L�mites de la habitaci�n (x, y, z)
    public float floatSpeed = 1.5f; // Velocidad del efecto de "flotaci�n"
    public float changeDirectionInterval = 2f; // Intervalo de tiempo para cambiar de direcci�n

    private Vector3 targetPosition; // La posici�n a la que el fantasma se mover�
    private bool isStopped = false; // Estado para saber si el fantasma est� detenido

    void Start()
    {
        // Inicializa una posici�n aleatoria como objetivo
        SetRandomTargetPosition();

        // Cambia de direcci�n peri�dicamente
        InvokeRepeating(nameof(SetRandomTargetPosition), changeDirectionInterval, changeDirectionInterval);
    }

    void Update()
    {
        if (!isStopped)
        {
            // Mueve al fantasma hacia la posici�n objetivo
            MoveTowardsTarget();

            // Efecto de flotaci�n vertical
            FloatEffect();
        }
    }

    private void MoveTowardsTarget()
    {
        // Mueve el fantasma hacia la posici�n objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si llega al objetivo, calcula un nuevo objetivo aleatorio
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private void FloatEffect()
    {
        // Peque�o movimiento vertical (efecto de flotaci�n)
        transform.position += new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * 0.01f, 0);
    }

    private void SetRandomTargetPosition()
    {
        // Genera una posici�n aleatoria dentro de los l�mites de la habitaci�n
        targetPosition = new Vector3(
            Random.Range(-roomBounds.x, roomBounds.x),
            Random.Range(0f, roomBounds.y), // Aseg�rate de que se mantenga dentro de los l�mites verticales
            Random.Range(-roomBounds.z, roomBounds.z)
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // Si golpea una pared
        {
            targetPosition = -targetPosition; // Cambia la direcci�n
        }

        // Verifica si el objeto que colision� tiene el tag "BurbujaBala"
        if (collision.gameObject.CompareTag("BurbujaBala"))
        {
            StopGhost(); // Detener al fantasma
        }
    }

    private void StopGhost()
    {
        isStopped = true; // Cambiar el estado para detener el movimiento
        speed = 0f; // Asegurarse de que la velocidad sea 0
        //Destroy(gameObject, 2f); // Destruir el fantasma despu�s de 2 segundos
    }
}*/

public class SlowGhostMovement : MonoBehaviour
{
    public float speed = 1f; // Velocidad lenta del fantasma
    public float circleRadius = 2f; // Radio del c�rculo
    public float circleSpeed = 1f; // Velocidad de rotaci�n alrededor del c�rculo
    public Vector3 centerPoint; // Centro del movimiento circular

    private float angle; // �ngulo actual del movimiento circular

    void Start()
    {
        // Establece el centro inicial como la posici�n del fantasma
        centerPoint = transform.position;
    }

    void Update()
    {
        MoveInCircle();
    }

    private void MoveInCircle()
    {
        // Incrementa el �ngulo basado en la velocidad
        angle += circleSpeed * Time.deltaTime;

        // Calcula la posici�n en el c�rculo
        float x = Mathf.Cos(angle) * circleRadius;
        float z = Mathf.Sin(angle) * circleRadius;

        // Actualiza la posici�n del fantasma
        transform.position = Vector3.MoveTowards(transform.position, centerPoint + new Vector3(x, 0, z), speed * Time.deltaTime);
    }
}

public class ModerateGhostMovement : MonoBehaviour
{
    public float speed = 3f; // Velocidad moderada del fantasma
    public float zigzagAmplitude = 2f; // Amplitud del zigzag
    public float zigzagFrequency = 2f; // Frecuencia del zigzag

    private Vector3 direction; // Direcci�n principal del movimiento
    private float time; // Contador de tiempo para el zigzag

    void Start()
    {
        // Direcci�n inicial aleatoria
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

        // Direcci�n del zigzag
        Vector3 zigzagOffset = new Vector3(direction.z, 0, -direction.x) * offset;

        // Mueve al fantasma en la direcci�n principal con el zigzag a�adido
        transform.position += (direction + zigzagOffset).normalized * speed * Time.deltaTime;

        // Incrementa el tiempo para el zigzag
        time += Time.deltaTime;
    }
}

public class FastGhostMovement : MonoBehaviour
{
    public float speed = 6f; // Velocidad r�pida del fantasma
    public float circleRadius = 3f; // Radio del movimiento circular
    public float circleSpeed = 2f; // Velocidad del movimiento circular
    public float zigzagAmplitude = 2f; // Amplitud del zigzag
    public float zigzagFrequency = 3f; // Frecuencia del zigzag
    public Transform player; // Referencia al jugador

    private float angle; // �ngulo para el movimiento circular
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
        // Determina si debe esconderse (por ejemplo, si est� cerca del jugador)
        return Vector3.Distance(transform.position, player.position) < 10f;
    }

    private void HideBehindPlayer()
    {
        hidingBehindPlayer = true;
        Vector3 hidePosition = player.position - player.forward * 2f; // Posici�n detr�s del jugador
        transform.position = Vector3.MoveTowards(transform.position, hidePosition, speed * Time.deltaTime);
        hidingBehindPlayer = false;
    }
}

