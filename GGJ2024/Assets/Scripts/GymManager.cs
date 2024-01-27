using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymManager : MonoBehaviour
{
    [Header("Difficulty Settings")]
    [SerializeField] private int promptLength;
    [SerializeField] private float promptTime; // Most likely to be const but Just in Case
    [SerializeField] private GameObject[] promptElements;

    [Space]
    private int[][] inputs;

    // Left = 276, Right = 275, Down = 274, Up = 273
 
    // Start is called before the first frame update
    void Start()
    {
        // Generate Inputs
        GeneratePrompts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GeneratePrompts()
    {
        foreach (int[] prompt in inputs)
        {
            for (int i = 0; i < prompt.Length; i++)
            {
                GameObject randomElement = promptElements[Random.Range(0, promptElements.Length)];

                // Update UI prompt

                // Update prompt array
                prompt[i] = int.Parse(randomElement.tag);
            }
        }
    }
}
