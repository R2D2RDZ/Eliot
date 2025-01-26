using UnityEngine;

public class FastGhostMovement : MonoBehaviour
{
    public float speed = 6f; // Velocidad rápida del fantasma
    public float circleRadius = 3f; // Radio del movimiento circular
    public float circleSpeed = 2f; // Velocidad del movimiento circular
    public float zigzagAmplitude = 2f; // Amplitud del zigzag
    public float zigzagFrequency = 3f; // Frecuencia del zigzag
    private Transform player; // Referencia al jugador

    private float angle; // Ángulo para el movimiento circular
    private float time; // Contador de tiempo para el zigzag
    private bool hidingBehindPlayer = false; // Estado de esconderse

    void Start()
    {
        // Busca al jugador automáticamente por el tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con el tag 'Player'.");
        }
    }

    void Update()
    {
        if (!hidingBehindPlayer)
        {
            // Decide el tipo de movimiento basado en tiempo
            if (Time.time % 5 < 2.5f)
                MoveInCircle();
            else
                MoveInZigzag();
        }

        if (ShouldHide())
            HideBehindPlayer();
    }

    private void MoveInCircle()
    {
        angle += circleSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * circleRadius;
        float z = Mathf.Sin(angle) * circleRadius;
        transform.position += new Vector3(x, 0, z) * speed * Time.deltaTime;
    }

    private void MoveInZigzag()
    {
        float offset = Mathf.Sin(time * zigzagFrequency) * zigzagAmplitude;
        Vector3 direction = transform.forward + new Vector3(offset, 0, 0);
        transform.position += direction.normalized * speed * Time.deltaTime;
        time += Time.deltaTime;
    }

    private bool ShouldHide()
    {
        // Determina si debe esconderse (por ejemplo, si está cerca del jugador)
        return Vector3.Distance(transform.position, player.position) < 10f;
    }

    private void HideBehindPlayer()
    {
        hidingBehindPlayer = true;
        Vector3 hidePosition = player.position - player.forward * 2f; // Posición detrás del jugador
        transform.position = Vector3.MoveTowards(transform.position, hidePosition, speed * Time.deltaTime);
        hidingBehindPlayer = false;
    }
}
