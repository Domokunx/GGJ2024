using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickYourDateManager : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles;
    [HideInInspector] public static bool[] selected;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip click, correct;


    // Start is called before the first frame update
    void Start()
    {
        selected = new bool[toggles.Length];

        foreach (Toggle btn in toggles)
        {
            btn.interactable = false;
        }

        for (int i = 0; i < toggles.Length; i++)
        {
            if (GameManager.rizzed[i])
            {
                toggles[i].interactable = true;
            }
        }
    }

    public void FinaliseDecision()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                audioSource.clip = correct;
                audioSource.Play();
                selected[i] = true;
            }
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
