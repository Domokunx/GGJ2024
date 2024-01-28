using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    public static DifficultySettings instance;

    private OutfitScoreManager scoreManager;

    // index 0 = easy, 1 = medium, 2 = hard
    [Header("Typing Game Settings")]
    public int[] typingTimer;

    [Space]
    [Header("Waldo Game Settings")]
    public int[] waldoTimer;

    [Space]
    [Header("Gym Game Settings")]
    public int[] promptLength;
    public float[] promptTimer;
    public int[] rounds;

    [Space]
    [Header("Brain Game Settings")]
    public float[] solveTimeInterval;
    public int[] questionCount;

    [HideInInspector] public int difficulty;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetDifficulty()
    {
        scoreManager = FindAnyObjectByType<OutfitScoreManager>();
        if (scoreManager.GetScore() >= 7)
        {
            difficulty = 0;
        }
        else if (scoreManager.GetScore() >= 4)
        {
            difficulty = 1;
        }
        else difficulty = 2;
        Debug.Log(difficulty);
        Debug.Log(scoreManager.GetScore());
    }
}