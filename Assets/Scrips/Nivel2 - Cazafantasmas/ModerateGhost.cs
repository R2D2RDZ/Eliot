using UnityEngine;

public class ModerateGhost : GhostMovement
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
}

