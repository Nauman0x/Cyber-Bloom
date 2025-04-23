using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FibonacciPuzzle : MonoBehaviour
{
    public GameObject panel;

    public GameObject greenLightOn;
    public GameObject greenLightOff;
    public GameObject redLightOn;
    public GameObject redLightOff;

    public AudioSource buttonAudioSource; // 🔊 Assign in Inspector
    public AudioClip buttonClip;          // 🔊 Optional override clip

    private Collider2D signCollider;

    private List<int> fibonacciSequence = new List<int> { 1, 1, 2, 3, 5, 8, 13 };
    private int currentIndex = 0;
    private bool playerInRange = false;

    void Start()
    {
        signCollider = GetComponent<Collider2D>();
        panel.SetActive(false);

        greenLightOn.SetActive(false);
        greenLightOff.SetActive(true);
        redLightOn.SetActive(false);
        redLightOff.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("player"))
        {
            playerInRange = true;
            panel.SetActive(true);
            signCollider.enabled = false;
            Debug.Log("Player entered signboard zone. Panel opened.");
        }
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        Debug.Log("Panel manually opened.");
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Debug.Log("Panel manually closed.");
    }

    public void OnButtonPressed(int number)
    {
        Debug.Log("Button clicked with value: " + number);

        if (!playerInRange)
        {
            Debug.Log("Player not in range. Ignoring input.");
            return;
        }

        if (number == fibonacciSequence[currentIndex])
        {
            Debug.Log($"Correct number! Expected: {fibonacciSequence[currentIndex]}");
            TriggerLight(true);
            currentIndex++;

            if (currentIndex == fibonacciSequence.Count)
            {
                Debug.Log("Fibonacci sequence complete. Loading next scene...");
                SceneManager.LoadScene("SkyScene");
            }
        }
        else
        {
            Debug.Log($"Wrong number! Expected: {fibonacciSequence[currentIndex]}, but got: {number}");
            TriggerLight(false);
            currentIndex = 0;
        }
    }

    private void TriggerLight(bool correct)
    {
        if (correct)
        {
            greenLightOn.SetActive(true);
            greenLightOff.SetActive(false);

            redLightOn.SetActive(false);
            redLightOff.SetActive(true);
        }
        else
        {
            greenLightOn.SetActive(false);
            greenLightOff.SetActive(true);

            redLightOn.SetActive(true);
            redLightOff.SetActive(false);
        }
    }

    // 🔊 New function to play sound from button
    public void PlayButtonSound()
    {
        if (buttonAudioSource != null)
        {
            if (buttonClip != null)
                buttonAudioSource.PlayOneShot(buttonClip);
            else
                buttonAudioSource.Play(); // Uses clip already on the AudioSource
        }
    }
}
