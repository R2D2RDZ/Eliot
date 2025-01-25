using UnityEngine;

/*public abstract class GhostMovement : MonoBehaviour
{
    public float speed; // Velocidad del fantasma
    public Vector3 roomBounds = new Vector3(24f, 8f, 24f); // Límites de la habitación
    protected Transform player; // Referencia al jugador
    protected bool isStopped = false; // Estado para detener el movimiento

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Update()
    {
        if (!isStopped)
        {
            Move();
            HandleObstacles();
        }
    }

    protected abstract void Move(); // Cada fantasma define su movimiento

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
        Destroy(gameObject, 2f); // Destruir tras 2 segundos
    }

    private void HandleObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            if (hit.collider.CompareTag("Muebles"))
            {
                transform.Rotate(0, Random.Range(45f, 135f), 0);
            }
        }
    }
}*/

/*public abstract class GhostMovement : MonoBehaviour
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
}*/

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
        Destroy(gameObject, 2f); // Destruir el fantasma después de 2 segundos
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



