using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public int health = 3;
    public AudioSource track1;
    public AudioSource track2;
    public AudioSource track3;


    private void Update()
    {
        if (track1.pitch <= 2)
        {
            StartCoroutine(StartSpeed(track1, 1500, 2));
        }
        if (track2.pitch <= 2)
        {
            StartCoroutine(StartSpeed(track2, 1500, 2));
        }
        if (track3.pitch <= 2)
        {
            StartCoroutine(StartSpeed(track3, 1500, 2));
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Decrease lives on click, but reset lives to 3 after clicking passed 0
        if ((other.CompareTag("Obstacle"))) 
            {
            health -= 1;
        }
        switch (health)
        {
            case 3:
                StartCoroutine(StartFade(track1, 1, 1));
                break;
            case 2:
                StartCoroutine(StartFade(track2, 1, 0));
                break;
            case 1:
                StartCoroutine(StartFade(track3, 1, 0));
                break;
            case 0:
                GameOver();
                break;
            default:
                Debug.Log("Something went wrong");
                break;
        }
    } 

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public static IEnumerator StartSpeed(AudioSource audioSource, float duration, float targetPitch)
    {
        float currentTime = 0;
        float start = audioSource.pitch;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.pitch = Mathf.Lerp(start, targetPitch, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    private void GameOver()
    {
        track1.volume = 0;
        track2.volume = 0;
        track3.volume = 0;
    }
}