using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    public AudioSource audioSource;
    public AudioClip click;


    public void OpenCredits()
    {
        pauseScreen.SetActive(true);
        audioSource.clip = click;
        audioSource.Play();
    }

    public void CloseCredits()
    {
        pauseScreen.SetActive(false);
        audioSource.clip = click;
        audioSource.Play();
    }
}
