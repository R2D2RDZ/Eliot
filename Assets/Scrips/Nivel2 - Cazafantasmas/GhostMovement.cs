using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad del fantasma
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
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Muebles")) // Si golpea una pared
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
        Destroy(gameObject, 2f); // Destruir el fantasma despu�s de 2 segundos
    }
}