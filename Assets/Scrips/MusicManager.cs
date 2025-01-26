using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Clips de m�sica")]
    public AudioClip initialMusic; // M�sica por defecto
    public AudioClip actionMusic; // M�sica al agarrar el arma
    public AudioClip endMusic; // M�sica cuando no hay fantasmas

    [Header("Configuraci�n del audio")]
    public AudioSource audioSource; // Componente de AudioSource para reproducir la m�sica

    [Header("Referencias externas")]
    public VRGun gunScript; // Referencia al script del arma
    public Ghost[] ghosts; // Lista de fantasmas en la escena

    void Start()
    {
        // Verifica que el AudioSource est� asignado
        if (audioSource == null)
        {
            Debug.LogError("Falta el componente AudioSource en el MusicManager.");
            return;
        }

        // Inicia la m�sica por defecto
        PlayDefaultMusic();
    }

    void Update()
    {
        if (gunScript != null && gunScript.isGripped)
        {
            // Cambiar a la m�sica de combate si el arma est� agarrada
            PlayMusic(actionMusic);
        }
        else if (NoGhostsAlive())
        {
            // Cambiar a la m�sica calmada si no hay fantasmas
            PlayMusic(endMusic);
        }
        else
        {
            // Volver a la m�sica por defecto
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
