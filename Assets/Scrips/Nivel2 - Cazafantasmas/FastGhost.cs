using UnityEngine;

/*public class FastGhost : GhostMovement
{
    private float angle = 0f;
    private float zigzagTimer = 0f;
    private int direction = 1;

    protected override void Move()
    {
        // Movimiento circular
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * roomBounds.x / 6f;
        float z = Mathf.Sin(angle) * roomBounds.z / 6f;
        transform.position += new Vector3(x, 0, z) * Time.deltaTime;

        // Movimiento en zigzag
        zigzagTimer += Time.deltaTime;
        if (zigzagTimer > 0.5f)
        {
            direction *= -1;
            zigzagTimer = 0f;
        }
        transform.position += transform.right * direction * speed * 0.3f * Time.deltaTime;

        // Esconderse del jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < 5f)
        {
            Vector3 hideDirection = (transform.position - player.position).normalized;
            transform.position += hideDirection * speed * Time.deltaTime;
        }
    }
}*/

public class FastGhost : GhostMovement
{
    private float angle = 0f; // Para movimiento circular
    private float zigzagTimer = 0f; // Para zigzag
    private int direction = 1; // Dirección del zigzag

    void Awake()
    {
        speed = 15f; // Velocidad para fantasmas lentos
    }
    protected override void Move()
    {
        // Combinación de círculo y zigzag
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * roomBounds.x / 6f; // Movimiento circular en X
        float z = Mathf.Sin(angle) * roomBounds.z / 6f; // Movimiento circular en Z
        transform.position += new Vector3(x, transform.position.y, z) * Time.deltaTime;

        zigzagTimer += Time.deltaTime;
        if (zigzagTimer > 0.5f) // Zigzag rápido
        {
            direction *= -1;
            zigzagTimer = 0f;
        }
        transform.position += transform.right * direction * speed * 0.3f * Time.deltaTime;

        // Esconderse si está cerca del jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < 5f) // Distancia para esconderse
        {
            Vector3 hideDirection = (transform.position - player.position).normalized;
            transform.position += hideDirection * speed * Time.deltaTime;
        }
    }
}


