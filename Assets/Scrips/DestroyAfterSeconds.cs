using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] float seconds;

    [Header("Clips de música")]
    public AudioClip popSound; // Música por defecto

    [Header("Configuración del audio")]
    public AudioSource audioSource; // Componente de AudioSource para reproducir la música
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
