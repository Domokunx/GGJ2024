using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    public static DifficultySettings instance;

    // index 0 = easy, 1 = medium, 2 = hard
    [Header("Typing Game Settings")]
    [SerializeField] public float[] typingTimer;

    [Space]
    [Header("Waldo Game Settings")]
    public float[] waldoTimer;

    [Space]
    [Header("Gym Game Settings")]
    public int[] promptLength;
    public float[] promptTimer;
    public int[] rounds;

    [Space]
    [Header("Brain Game Settings")]
    public float[] solveTimeInterval;
    public int[] questionCount;

    public int difficulty = 0; // Where get outfitscore

    private void Awake()
    {
        instance = this;
    }
}