using UnityEngine;

/*public class SlowGhost : GhostMovement
{
    private float angle = 0f; // �ngulo para el movimiento circular

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
    public Vector3 roomBounds = new Vector3(24f, 8f, 24f); // L�mites de la habitaci�n (x, y, z)
    public float floatSpeed = 1.5f; // Velocidad del efecto de "flotaci�n"
    public float changeDirectionInterval = 2f; // Intervalo de tiempo para cambiar de direcci�n
    public string furnitureTag; // Tag de los muebles
    public string bulletTag; // Tag de las balas

    private Vector3 targetPosition; // La posici�n a la que el fantasma se mover�
    private bool isStopped = false; // Estado para saber si el fantasma est� detenido

    void Start()
    {
        SetRandomTargetPosition(); // Inicializa una posici�n aleatoria como objetivo
        InvokeRepeating(nameof(SetRandomTargetPosition), changeDirectionInterval, changeDirectionInterval); // Cambia de direcci�n peri�dicamente
    }

    void Update()
    {
        if (!isStopped)
        {
            MoveTowardsTarget(); // Mueve al fantasma
            FloatEffect(); // Aplica el efecto de flotaci�n
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
        // Movimiento vertical para el efecto de flotaci�n
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

