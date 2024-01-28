using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnEnable : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dateOutcome;
    // Start is called before the first frame update
    void OnEnable()
    {
        audioSource.clip = dateOutcome;
        audioSource.Play();
    }

}
