using UnityEngine;

/*public class SlowGhost : GhostMovement
{
    private float angle = 0f; // �ngulo para el movimiento circular

    protected override void Move()
    {
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * roomBounds.x / 4f;
        float z = Mathf.Sin(angle) * roomBounds.z / 4f;
        transform.position = new Vector3(x, transform.position.y, z);
    }
}*/

using System.Collections.Generic;

public class SlowGhost : MonoBehaviour
{
    [Header("Configuraci�n de fantasmas")]
    public GameObject ghostPrefab; // Prefab de los fantasmas
    public int ghostCount = 5; // Cantidad de fantasmas a generar
    public float spawnRadius = 0.5f; // Radio de aparici�n de los fantasmas
    public float moveSpeed = 1f; // Velocidad de movimiento circular de los fantasmas
    public float rotationSpeed = 50f; // Velocidad de rotaci�n del movimiento circular

    private List<GameObject> spawnedGhosts = new List<GameObject>(); // Lista para almacenar los fantasmas generados

    [Header("Restricciones de la habitaci�n")]
    public Vector3 roomCenter = Vector3.zero; // Centro de la habitaci�n
    public Vector3 roomSize = new Vector3(24f, 8f, 24f); // Dimensiones de la habitaci�n

    [Header("Detecci�n de colisiones")]
    public string furnitureTag = "Mueble"; // Tag de objetos a esquivar
    public string wallTag = "Wall"; // Tag de las paredes de la habitaci�n
    public string bubbleBulletTag = "BalaBurbuja"; // Tag de proyectiles que detendr�n a los fantasmas

    void Update()
    {
        MoveGhostsInCircle();
    }

    // M�todo para generar los fantasmas
    public void SpawnGhosts()
    {
        if (spawnedGhosts.Count > 0) return; // Evitar que se generen fantasmas m�ltiples veces

        for (int i = 0; i < ghostCount; i++)
        {
            Vector3 spawnPosition = GetRandomPositionInRoom();
            GameObject ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
            spawnedGhosts.Add(ghost);
        }
    }

    // Obtener una posici�n aleatoria dentro de la habitaci�n
    private Vector3 GetRandomPositionInRoom()
    {
        float x = Random.Range(roomCenter.x - roomSize.x / 2, roomCenter.x + roomSize.x / 2);
        float y = roomCenter.y; // Mantener la altura fija
        float z = Random.Range(roomCenter.z - roomSize.z / 2, roomCenter.z + roomSize.z / 2);
        return new Vector3(x, y, z);
    }

    // Movimiento circular de los fantasmas
    private void MoveGhostsInCircle()
    {
        foreach (GameObject ghost in spawnedGhosts)
        {
            if (ghost != null)
            {
                // Movimiento circular en torno al centro de la habitaci�n
                ghost.transform.RotateAround(roomCenter, Vector3.up, rotationSpeed * Time.deltaTime);

                // Limitar posici�n dentro de la habitaci�n
                Vector3 position = ghost.transform.position;

                if (!IsWithinRoomBounds(position))
                {
                    Vector3 directionToCenter = (roomCenter - position).normalized;
                    ghost.transform.position += directionToCenter * moveSpeed * Time.deltaTime;
                }

                // Esquivar objetos con el tag "Mueble"
                if (Physics.Raycast(ghost.transform.position, ghost.transform.forward, out RaycastHit hit, 1f))
                {
                    if (hit.collider.CompareTag(furnitureTag))
                    {
                        ghost.transform.Rotate(Vector3.up, 180f); // Cambiar direcci�n
                    }
                }

                // Detectar colisi�n con paredes
                if (Physics.Raycast(ghost.transform.position, ghost.transform.forward, out RaycastHit wallHit, 1f))
                {
                    if (wallHit.collider.CompareTag(wallTag))
                    {
                        ghost.transform.Rotate(Vector3.up, 180f); // Cambiar direcci�n
                    }
                }
            }
        }
    }

    // Verificar si un fantasma est� dentro de los l�mites de la habitaci�n
    private bool IsWithinRoomBounds(Vector3 position)
    {
        return position.x >= roomCenter.x - roomSize.x / 2 &&
               position.x <= roomCenter.x + roomSize.x / 2 &&
               position.y >= roomCenter.y - roomSize.y / 2 &&
               position.y <= roomCenter.y + roomSize.y / 2 &&
               position.z >= roomCenter.z - roomSize.z / 2 &&
               position.z <= roomCenter.z + roomSize.z / 2;
    }

    // M�todo para detener a un fantasma cuando es impactado
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(bubbleBulletTag))
        {
            foreach (GameObject ghost in spawnedGhosts)
            {
                if (collision.collider.gameObject == ghost)
                {
                    ghost.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; // Detener movimiento
                    ghost.GetComponent<Rigidbody>().isKinematic = true; // Desactivar f�sica
                }
            }
        }
    }
}
