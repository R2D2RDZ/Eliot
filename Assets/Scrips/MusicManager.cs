using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("Archivos de Música")]
    public AudioClip initialMusic; // Música inicial
    public AudioClip actionMusic;  // Música de acción cuando aparecen fantasmas
    public AudioClip endMusic;     // Música final cuando todos los fantasmas han desaparecido

    private AudioSource audioSource; // Componente de AudioSource
    private int ghostCount = 0;      // Número de fantasmas activos en la escena

    void Start()
    {
        // Configurar el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        PlayMusic(initialMusic); // Reproducir la música inicial
    }

    public void UpdateGhostCount(int count)
    {
        ghostCount = count;

        if (ghostCount > 0 && audioSource.clip != actionMusic)
        {
            PlayMusic(actionMusic); // Cambiar a la música de acción
        }
        else if (ghostCount == 0 && audioSource.clip != endMusic)
        {
            PlayMusic(endMusic); // Cambiar a la música final
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
