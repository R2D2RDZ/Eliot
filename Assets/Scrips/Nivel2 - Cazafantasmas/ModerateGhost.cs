using UnityEngine;

/*public class ModerateGhost : GhostMovement
{
    private float zigzagTimer = 0f;
    private int direction = 1;

    protected override void Move()
    {
        zigzagTimer += Time.deltaTime;
        if (zigzagTimer > 1f)
        {
            direction *= -1;
            zigzagTimer = 0f;
        }

        Vector3 forward = transform.forward * speed * Time.deltaTime;
        Vector3 sideways = transform.right * direction * speed * 0.5f * Time.deltaTime;
        transform.position += forward + sideways;
    }
}*/

public class ModerateGhost : GhostMovement
{
    private float zigzagTimer = 0f; // Temporizador para cambiar de direcci�n
    private int direction = 1; // Direcci�n del zigzag

    void Awake()
    {
        speed = 10f; // Velocidad para fantasmas lentos
    }
    protected override void Move()
    {
        zigzagTimer += Time.deltaTime;
        if (zigzagTimer > 1f) // Cambiar direcci�n cada segundo
        {
            direction *= -1;
            zigzagTimer = 0f;
        }

        // Movimiento hacia adelante con desviaci�n en zigzag
        Vector3 forward = transform.forward * speed * Time.deltaTime;
        Vector3 sideways = transform.right * direction * speed * 0.5f * Time.deltaTime;
        transform.position += forward + sideways;
    }
}

