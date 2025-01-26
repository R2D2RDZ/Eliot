using UnityEngine;
using UnityEngine.SceneManagement;

public class Pasillo : MonoBehaviour
{
    private int ghostCount = 0; // Número de fantasmas activos en la escena

    void Start()
    {
        // Inicializar el conteo de fantasmas
        ghostCount = Object.FindObjectsByType<GhostMovement>(FindObjectsSortMode.None).Length;
    }

    public void DecreaseGhostCount()
    {
        ghostCount--;

        // Cambiar de escena si no quedan fantasmas
        if (ghostCount <= 0)
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Pasillo"); // Cambiar a la escena "Pasillo"
    }
}

