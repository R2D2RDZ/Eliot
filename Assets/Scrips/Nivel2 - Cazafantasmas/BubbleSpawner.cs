using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab; // Prefab de la burbuja
    private GameObject door; // Referencia al objeto de la puerta

    void Start()
    {
        // Encuentra la puerta por el tag
        door = GameObject.FindGameObjectWithTag("door");
        if (door == null)
        {
            Debug.LogError("No se encontró un objeto con el tag 'door'. Asegúrate de asignar correctamente el tag.");
        }
    }

    void Update()
    {
        // Revisa si no quedan fantasmas en la escena
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        if (ghosts.Length == 0)
        {
            SpawnBubble();
        }
    }

    void SpawnBubble()
    {
        // Verifica si ya hay una burbuja existente
        GameObject existingBubble = GameObject.FindGameObjectWithTag("Bubble");
        if (existingBubble == null && door != null)
        {
            // Instancia la burbuja en la posición de la puerta
            Instantiate(bubblePrefab, door.transform.position, Quaternion.identity).tag = "Bubble";
            Debug.Log("Burbuja generada en la puerta.");
        }
    }
}


