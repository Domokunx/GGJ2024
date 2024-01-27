using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymManager : MonoBehaviour
{
    [Header("Difficulty Settings")]
    [SerializeField] private int promptLength;
    [SerializeField] private int rounds;
    [SerializeField] private float promptTime; // Most likely to be const but Just in Case
    [SerializeField] private GameObject[] promptElements;

    [Space]
    [SerializeField] private GameObject promptPanel;

    #region Private Variables
    private int[][] inputs;
    private int FIRST_ELEMENT_XPOSITION;
    private int LAST_ELEMENT_XPOSITION;

    private float timeToNextPrompt;

    private int inputIndex;

    private GameObject[] currentPrompts;
    #endregion

    // Left = 276, Right = 275, Down = 274, Up = 273

    // Start is called before the first frame update
    void Start()
    {
        // Initialise Variables
        FIRST_ELEMENT_XPOSITION = -450;
        LAST_ELEMENT_XPOSITION = 500;
        inputIndex = 0;
        timeToNextPrompt = 0;

        // Initialise Array
        inputs = new int[rounds][];
        currentPrompts = new GameObject[promptLength];

        // Generate Inputs
        GeneratePrompts();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToNextPrompt < Time.time) 
        {
            if (inputIndex + 1 == rounds)
            {
                GameManager.backToOutfitSelector();
                return;
            }

            timeToNextPrompt += promptTime;
            ResetPrompt();
            GeneratePromptUI();
            inputIndex++;
        }
    }

    private void GeneratePrompts()
    {
        for (int j = 0; j < rounds; j++) {
            {
                inputs[j] = new int[promptLength];

                for (int i = 0; i < promptLength; i++)
                {
                    GameObject randomElement = promptElements[Random.Range(0, promptElements.Length)];
                    // Update UI prompt
                    // Update prompt array
                    switch (randomElement.tag)
                    {
                        case "Left":
                            inputs[j][i] = 276;
                            break;

                        case "Right":
                            inputs[j][i] = 275;
                            break;

                        case "Down":
                            inputs[j][i] = 274;
                            break;

                        case "Up":
                            inputs[j][i] = 273;
                            break;

                        default:
                            Debug.Log("Somehow all cases missed");
                            break;
                    }
                }
            }
        }
    }
    private void GeneratePromptUI()
    {
        for (int i = 0; i < promptLength; i++)
        {
            Vector2 elementPosition = new Vector2(Mathf.Lerp(FIRST_ELEMENT_XPOSITION,
                                                             LAST_ELEMENT_XPOSITION,
                                                             (float) i / promptLength),
                                                          0);

            GameObject go;
            switch (inputs[inputIndex][i])
            {
                case 273:
                    go = Instantiate(promptElements[3], promptPanel.transform);
                    go.GetComponent<RectTransform>().anchoredPosition = elementPosition;
                    currentPrompts[i] = go;
                    break;

                case 274:
                    go = Instantiate(promptElements[2], promptPanel.transform);
                    go.GetComponent<RectTransform>().anchoredPosition = elementPosition;
                    currentPrompts[i] = go;
                    break;

                case 275:
                    go = Instantiate(promptElements[1], promptPanel.transform);
                    go.GetComponent<RectTransform>().anchoredPosition = elementPosition;
                    currentPrompts[i] = go;
                    break;

                case 276:
                    go = Instantiate(promptElements[0], promptPanel.transform);
                    go.GetComponent<RectTransform>().anchoredPosition = elementPosition;
                    currentPrompts[i] = go;
                    break;
            }
        }
    }
    private void ResetPrompt()
    {
        foreach (GameObject go in currentPrompts) {
            Destroy(go);
        }
    }
}
