using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("Archivos de M�sica")]
    public AudioClip initialMusic; // M�sica inicial
    public AudioClip actionMusic;  // M�sica de acci�n cuando aparecen fantasmas
    public AudioClip endMusic;     // M�sica final cuando todos los fantasmas han desaparecido

    private AudioSource audioSource; // Componente de AudioSource
    private int ghostCount = 0;      // N�mero de fantasmas activos en la escena

    void Start()
    {
        // Configurar el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        PlayMusic(initialMusic); // Reproducir la m�sica inicial
    }

    public void UpdateGhostCount(int count)
    {
        ghostCount = count;

        if (ghostCount > 0 && audioSource.clip != actionMusic)
        {
            PlayMusic(actionMusic); // Cambiar a la m�sica de acci�n
        }
        else if (ghostCount == 0 && audioSource.clip != endMusic)
        {
            PlayMusic(endMusic); // Cambiar a la m�sica final
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
