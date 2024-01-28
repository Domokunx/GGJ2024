using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip click;

    void Start()
    {
        
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("OutfitSelector");
    }

    public void BeginToGame()
    {
        audioSource.clip = click;
        audioSource.Play();
        StartCoroutine(ChangeScene());
    }
}
