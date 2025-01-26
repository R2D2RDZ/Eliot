using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab; // Prefab de la burbuja
    private GameObject door; // Referencia al objeto de la puerta
    public Vector3 offset = new Vector3(0, 2, 0);
    public GameObject[] ghosts;

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
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
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
            GameObject bubble = Instantiate(bubblePrefab, door.transform.position + offset, Quaternion.identity);
            bubble.tag = "Bubble";
            bubble.GetComponent<destroyBubble>().sceneToLoad = "Pasillo";
            Debug.Log("Burbuja generada en la puerta.");
        }
    }
}


