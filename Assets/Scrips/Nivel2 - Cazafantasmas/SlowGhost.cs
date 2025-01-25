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

public class SlowGhost : GhostMovement
{
    private float angle = 0f; // Ángulo para el movimiento circular

    protected override void Move()
    {
        angle += speed * Time.deltaTime; // Incrementar ángulo según la velocidad
        float x = Mathf.Cos(angle) * roomBounds.x / 4f; // Movimiento en X
        float z = Mathf.Sin(angle) * roomBounds.z / 4f; // Movimiento en Z
        transform.position = new Vector3(x, transform.position.y, z);
    }
}


