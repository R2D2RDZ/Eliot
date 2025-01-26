using UnityEngine;
public class ModerateGhostMovement : MonoBehaviour
{
    public float speed = 3f; // Velocidad moderada del fantasma
    public float zigzagAmplitude = 2f; // Amplitud del zigzag
    public float zigzagFrequency = 2f; // Frecuencia del zigzag

    private Vector3 direction; // Direcci�n principal del movimiento
    private float time; // Contador de tiempo para el zigzag

    void Start()
    {
        // Direcci�n inicial aleatoria
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        MoveInZigzag();
    }

    private void MoveInZigzag()
    {
        // Calcula el desplazamiento en zigzag
        float offset = Mathf.Sin(time * zigzagFrequency) * zigzagAmplitude;

        // Direcci�n del zigzag
        Vector3 zigzagOffset = new Vector3(direction.z, 0, -direction.x) * offset;

        // Mueve al fantasma en la direcci�n principal con el zigzag a�adido
        transform.position += (direction + zigzagOffset).normalized * speed * Time.deltaTime;

        // Incrementa el tiempo para el zigzag
        time += Time.deltaTime;
    }
}