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

public class ModerateGhost : MonoBehaviour
{
    public float speed = 4f; // Velocidad moderada
    public float zigzagDistance = 3f; // Amplitud del zigzag

    private float zigzagTime;

    void Update()
    {
        zigzagTime += Time.deltaTime;
        Vector3 zigzagMovement = new Vector3(Mathf.Sin(zigzagTime * speed) * zigzagDistance, 0, speed * Time.deltaTime);
        Vector3 nextPosition = transform.position + zigzagMovement;

        if (!Physics.CheckSphere(nextPosition, 0.5f)) // Evita colisiones con muebles
        {
            transform.position = nextPosition;
        }
    }
}

