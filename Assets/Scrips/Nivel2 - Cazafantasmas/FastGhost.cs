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

public class FastGhost : MonoBehaviour
{
    public float speed = 6f;
    public float zigzagDistance = 3f;
    public float hideDistance = 2f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Encuentra al jugador
    }

    void Update()
    {
        Vector3 toPlayer = player.position - transform.position;

        if (toPlayer.magnitude > hideDistance) // Movimiento en zigzag si está lejos del jugador
        {
            Vector3 zigzagMovement = new Vector3(Mathf.Sin(Time.time * speed) * zigzagDistance, 0, speed * Time.deltaTime);
            transform.position += zigzagMovement;
        }
        else // Movimiento para esconderse detrás del jugador
        {
            Vector3 hidePosition = player.position - toPlayer.normalized * hideDistance;
            transform.position = Vector3.Lerp(transform.position, hidePosition, Time.deltaTime * speed);
        }
    }
}


