using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Clips de música")]
    public AudioClip initialMusic; // Música por defecto
    public AudioClip actionMusic; // Música al agarrar el arma
    public AudioClip endMusic; // Música cuando no hay fantasmas

    [Header("Configuración del audio")]
    public AudioSource audioSource; // Componente de AudioSource para reproducir la música

    [Header("Referencias externas")]
    public VRGun gunScript; // Referencia al script del arma
    public Ghost[] ghosts; // Lista de fantasmas en la escena

    void Start()
    {
        // Verifica que el AudioSource esté asignado
        if (audioSource == null)
        {
            Debug.LogError("Falta el componente AudioSource en el MusicManager.");
            return;
        }

        // Inicia la música por defecto
        PlayDefaultMusic();
    }

    void Update()
    {
        if (gunScript != null && gunScript.isGripped)
        {
            // Cambiar a la música de combate si el arma está agarrada
            PlayMusic(actionMusic);
        }
        else if (NoGhostsAlive())
        {
            // Cambiar a la música calmada si no hay fantasmas
            PlayMusic(endMusic);
        }
        else
        {
            // Volver a la música por defecto
            PlayMusic(initialMusic);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void PlayDefaultMusic()
    {
        PlayMusic(initialMusic);
    }

    private bool NoGhostsAlive()
    {
        // Verifica si todos los fantasmas han sido destruidos
        foreach (Ghost ghost in ghosts)
        {
            if (ghost != null) return false;
        }
        return true;
    }
}
