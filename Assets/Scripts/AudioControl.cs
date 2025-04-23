using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour
{
    public AudioSource bgMusic;
    public AudioSource narration;

    void Start()
    {
        // Start background music immediately
        if (bgMusic != null && bgMusic.clip != null)
        {
            bgMusic.Play();
            Debug.Log("Background music started.");
        }

        // If narration exists, handle it
        if (narration != null && narration.clip != null)
        {
            StartCoroutine(HandleNarration());
        }
    }

    private IEnumerator HandleNarration()
    {
        // Optional: wait for panel to open or trigger if needed
        yield return new WaitForSeconds(0.2f);

        narration.Play();
        Debug.Log("Narration started.");

        // Pause bgMusic only *after* narration starts
        if (bgMusic != null && bgMusic.isPlaying)
        {
            bgMusic.Pause();
            Debug.Log("Background music paused.");
        }

        // Wait for narration to finish
        yield return new WaitForSeconds(narration.clip.length);

        if (bgMusic != null)
        {
            bgMusic.Play();
            Debug.Log("Background music resumed.");
        }
    }
}
