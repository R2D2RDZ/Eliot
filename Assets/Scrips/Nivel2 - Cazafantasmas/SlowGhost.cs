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

/*public class SlowGhost : GhostMovement
{
    private float angle = 0f; // Ángulo para el movimiento circular
    void Awake()
    {
        speed = 5f; // Velocidad para fantasmas lentos
    }
    protected override void Move()
    {
        angle += speed * Time.deltaTime; // Incrementar ángulo según la velocidad
        float x = Mathf.Cos(angle) * roomBounds.x / 4f; // Movimiento en X
        float z = Mathf.Sin(angle) * roomBounds.z / 4f; // Movimiento en Z
        transform.position = new Vector3(x, transform.position.y, z);
    }
}*/

public class SlowGhost : MonoBehaviour
{
    public float speed = 2f; // Velocidad lenta
    public float radius = 5f; // Radio del movimiento circular

    private Vector3 centerPosition;

    void Start()
    {
        centerPosition = transform.position; // Guarda la posición inicial como centro del círculo
    }

    void Update()
    {
        float angle = Time.time * speed; // Calcula el ángulo de movimiento
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        Vector3 newPosition = new Vector3(centerPosition.x + x, transform.position.y, centerPosition.z + z);

        if (!Physics.CheckSphere(newPosition, 0.5f)) // Evita colisiones con muebles
        {
            transform.position = newPosition;
        }
    }
}

