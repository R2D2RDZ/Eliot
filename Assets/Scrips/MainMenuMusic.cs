using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [Header("Clips de m�sica")]
    public AudioClip defaultMusic; // M�sica por defecto

    [Header("Configuraci�n del audio")]
    public AudioSource audioSource; // Componente de AudioSource para reproducir la m�sica

    void Start()
    {
        // Verifica que el AudioSource est� asignado
        if (audioSource == null)
        {
            Debug.LogError("Falta el componente AudioSource en el MusicManager.");
            return;
        }

        // Inicia la m�sica por defecto
        PlayMusic(defaultMusic);
    }
    /*void Update()
    {
        if (gunScript != null && gunScript.isGripped)
        {
            // Cambiar a la m�sica de combate si el arma est� agarrada
            if (!isCombatMusicPlaying)
            {
                PlayMusic(combatMusic);
                isCombatMusicPlaying = true;
                isCalmMusicPlaying = false;
            }
        }
        else if (NoGhostsAlive())
        {
            // Cambiar a la m�sica calmada si no hay fantasmas
            if (!isCalmMusicPlaying)
            {
                PlayMusic(calmMusic);
                isCalmMusicPlaying = true;
                isCombatMusicPlaying = false;
            }
        }
        else
        {
            // Volver a la m�sica por defecto si no se cumplen las condiciones anteriores
            if (audioSource.clip != defaultMusic)
            {
                PlayMusic(defaultMusic);
                isCombatMusicPlaying = false;
                isCalmMusicPlaying = false;
            }
        }
    }*/

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.Stop();
            audioSource.loop = true; // Activa el loop para este clip
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    /*private bool NoGhostsAlive()
    {
        // Verifica si todos los fantasmas han sido destruidos
        foreach (Ghost ghost in ghosts)
        {
            if (ghost != null) return false;
        }
        return true;
    }*/
}