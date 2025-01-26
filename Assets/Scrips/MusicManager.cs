using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Clips de m�sica")]
    public AudioClip defaultMusic; // M�sica por defecto
    public AudioClip combatMusic; // M�sica al agarrar el arma
    
    [Header("Configuraci�n del audio")]
    public AudioSource audioSource; // Componente de AudioSource para reproducir la m�sica

    [Header("Referencias externas")]
    public VRGun gunScript; // Referencia al script del arma
    public Ghost[] ghosts; // Lista de fantasmas en la escena

    private bool isCombatMusicPlaying = false; // Bandera para evitar cambios constantes de m�sica
    private bool isCalmMusicPlaying = false; // Bandera para evitar cambios constantes de m�sica

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

    void Update()
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
    }

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
