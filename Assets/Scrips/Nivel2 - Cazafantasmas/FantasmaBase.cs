using UnityEngine;

public class Ghost : MonoBehaviour
{
    private bool isStopped = false;

    void Update()
    {
        if (isStopped) return;

        // L�gica de movimiento espec�fica (circulares, zigzag, etc.)
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaBurbuja"))
        {
            StopGhost();
            Destroy(other.gameObject); // Destruye la bala al impactar
        }
    }

    private void StopGhost()
    {
        isStopped = true;
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero; // Detener el movimiento f�sico
        // Opcional: Animaci�n o cambio visual para indicar que est� detenido
    }
}
