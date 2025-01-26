using UnityEngine;
using UnityEngine.SceneManagement;

public class LavelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BotonStart()
    {
        SceneManager.LoadScene(1);
    }
    public void BotonSalir()
    {
        Debug.Log("Salir del Juego");
        Application.Quit();
    }

    public void BotonCreditos()
    {
        SceneManager.LoadScene(2);
    }
}
