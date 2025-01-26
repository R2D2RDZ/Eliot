using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] float seconds;

    [Header("Clips de m�sica")]
    public AudioClip popSound; // M�sica por defecto

    [Header("Configuraci�n del audio")]
    public AudioSource audioSource; // Componente de AudioSource para reproducir la m�sica
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayMusic(popSound);
        Destroy(gameObject, seconds);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.Stop();
            audioSource.loop = false; // Activa el loop para este clip
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

}
