using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Achievements : MonoBehaviour
{
    private int totalScore = 0;
    
    [SerializeField] private GameObject achievement;

    private void Awake()
    {
        achievement.SetActive(false);
        totalScore = FindAnyObjectByType<DifficultySettings>().totalScore;

        if (totalScore == 0 && AllRizzed())
        {
            achievement.SetActive(true);
        }

    }

    private bool AllRizzed()
    {
        foreach (bool b in GameManager.rizzed)
        {
            if (!b) return false;
        }

        return true;
    }

    public void Restart()
    {
        GameManager.nextDate = 3;
        GameManager.rizzed = new bool[4];
        SceneManager.LoadScene(2);
    }
}
