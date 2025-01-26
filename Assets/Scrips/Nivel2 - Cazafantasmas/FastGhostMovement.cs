using UnityEngine;

public class FastGhostMovement : MonoBehaviour
{
    public float speed = 6f; // Velocidad rápida del fantasma
    public float circleRadius = 3f; // Radio del movimiento circular
    public float circleSpeed = 2f; // Velocidad del movimiento circular
    public float zigzagAmplitude = 2f; // Amplitud del zigzag
    public float zigzagFrequency = 3f; // Frecuencia del zigzag

    private float angle; // Ángulo para el movimiento circular
    private float time; // Contador de tiempo para el zigzag
    private bool isStopped = false; // Estado para saber si el fantasma está detenido

    void Update()
    {
        // Alterna entre movimiento circular y zigzag basado en tiempo
        if (Time.time % 5 < 2.5f)
            MoveInCircle();
        else
            MoveInZigzag();
    }

    private void MoveInCircle()
    {
        // Calcula el movimiento circular
        angle += circleSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * circleRadius;
        float z = Mathf.Sin(angle) * circleRadius;

        // Aplica el movimiento circular
        transform.position += new Vector3(x, 0, z) * speed * Time.deltaTime;
    }

    private void MoveInZigzag()
    {
        // Calcula el desplazamiento en zigzag
        float offset = Mathf.Sin(time * zigzagFrequency) * zigzagAmplitude;

        // Direccion principal + zigzag
        Vector3 direction = transform.forward + new Vector3(offset, 0, 0);
        transform.position += direction.normalized * speed * Time.deltaTime;

        // Incrementa el tiempo para el zigzag
        time += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BurbujaBala"))
        {
            StopGhost(); // Detener al fantasma
        }
    }

    private void StopGhost()
    {
        isStopped = true; // Cambiar el estado para detener el movimiento
        speed = 0f; // Asegurarse de que la velocidad sea 0
    }
}
