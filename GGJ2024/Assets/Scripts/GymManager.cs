using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GymManager : MonoBehaviour
{ 
    [SerializeField] private GameObject[] promptElements;
    [SerializeField] private GameObject promptPanel;
    [SerializeField] private Slider timer;
    [SerializeField] private GameObject winTransitionScreen;
    [SerializeField] private GameObject loseTransitionScreen;
    [SerializeField] private GameObject idleSprite;
    [SerializeField] private GameObject successSprite;

    public AudioSource audioSource;
    public AudioClip leftKey, rightKey, upKey, downKey, correct, wrong;

    #region Private Variables
    private int promptLength;
    private int rounds;
    private float promptTime; // Most likely to be const but Just in Case
    
    private int[] inputs;
    private int FIRST_ELEMENT_XPOSITION;
    private int LAST_ELEMENT_XPOSITION;

    private int score;

    private bool levelStart = false;
    private float countdown = 4f;

    private float timeToNextPrompt;

    private int inputIndex;
    private int elementIndex;

    private GameObject[] currentPrompts;
    #endregion

    // Left = 276, Right = 275, Down = 274, Up = 273

    // Start is called before the first frame update
    void Start()
    {
        SetDifficultySettings();
        // Initialise Variables
        timer.value = timer.maxValue = promptTime;
        FIRST_ELEMENT_XPOSITION = -450;
        LAST_ELEMENT_XPOSITION = 580;
        inputIndex = 0;
        timeToNextPrompt = countdown;
        elementIndex = 0;
        winTransitionScreen.SetActive(false);
        loseTransitionScreen.SetActive(false);
        score = 0;

        // Initialise Array
        inputs = new int[promptLength];
        currentPrompts = new GameObject[promptLength];

        // Generate Inputs
        GeneratePrompts();

        StartCoroutine(StartLevel(countdown));
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelStart) return;
        CheckKeyPress();

        // Timer for prompt change
        if (timeToNextPrompt < Time.timeSinceLevelLoad) 
        {
            if (currentPrompts[0] != null && CheckFinalInput())
            {
                StartCoroutine(PlaySuccessAnim());
                score++;
            }
                
            if (inputIndex == rounds)
            {
                if (score >= (float) rounds / 2)
                {
                    GameManager.rizzed[2] = true;
                    StartCoroutine(GameManager.BackToOutfitSelector(winTransitionScreen));
                } else { 
                    StartCoroutine(GameManager.BackToOutfitSelector(loseTransitionScreen));
                }

                timeToNextPrompt += 999999f;
                return;
            }

            ResetVariables();
        }

        timer.value -= Time.deltaTime;
    }
    private void SetDifficultySettings()
    {
        int difficulty = DifficultySettings.instance.difficulty;
        promptLength = DifficultySettings.instance.promptLength[difficulty];
        rounds = DifficultySettings.instance.rounds[difficulty];
        promptTime = DifficultySettings.instance.promptTimer[difficulty];
    }
    private IEnumerator PlaySuccessAnim()
    {
        Debug.Log("correct");
        audioSource.clip = correct;
        audioSource.Play();
        idleSprite.SetActive(false);
        successSprite.SetActive(true);

        yield return new WaitForSeconds(1f);

        idleSprite.SetActive(true);
        successSprite.SetActive(false);
    }
    private bool CheckFinalInput()
    {

        // All green, score +1;
        foreach (GameObject go in currentPrompts)
        {
            if (go.GetComponent<Image>().color != Color.white){
                Debug.Log("wrong");
                audioSource.clip = wrong;
                audioSource.Play();
                return false;
            } 

        }
        return true;
    }
    private void CheckKeyPress()
    {
        if (Input.anyKeyDown && promptLength == elementIndex)
        {
            // Reset Color
            foreach (GameObject go in currentPrompts)
            {
                go.GetComponent<Image>().color = Color.black;
            }
            // Reset index
            elementIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && inputs[elementIndex] == 276)
        {
            audioSource.clip = leftKey;
            audioSource.Play();

            Debug.Log("Left Checked");

            currentPrompts[elementIndex++].GetComponent<Image>().color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && inputs[elementIndex] == 275)
        {
            audioSource.clip = rightKey;
            audioSource.Play();

            Debug.Log("Right Checked");

            currentPrompts[elementIndex++].GetComponent<Image>().color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && inputs[elementIndex] == 274)
        {
            audioSource.clip = downKey;
            audioSource.Play();

            Debug.Log("Down Checked");

            currentPrompts[elementIndex++].GetComponent<Image>().color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && inputs[elementIndex] == 273)
        {
            audioSource.clip = upKey;
            audioSource.Play();

            Debug.Log("Up Checked");

            currentPrompts[elementIndex++].GetComponent<Image>().color = Color.white;
        } else if (currentPrompts[0] != null && Input.anyKeyDown)
        {
            // Reset Color
            foreach (GameObject go in currentPrompts)
            {
                go.GetComponent<Image>().color = Color.black;
            }
            // Reset index
            elementIndex = 0;
        }
    }
    private IEnumerator StartLevel(float countdown)
    {
        yield return new WaitForSeconds(countdown);
        levelStart = true;
    }
    private void ResetVariables()
    {
        timeToNextPrompt += promptTime;
        ResetPrompt();
        GeneratePrompts();
        GeneratePromptUI();
        inputIndex++;
        elementIndex = 0;
        timer.value = promptTime;
    }
    private void GeneratePrompts()
    {
        for (int i = 0; i < promptLength; i++)
        {
            GameObject randomElement = promptElements[Random.Range(0, promptElements.Length)];
            // Update UI prompt
            // Update prompt array
            switch (randomElement.tag)
            {
                case "Left":
                    inputs[i] = 276;
                    break;

                case "Right":
                    inputs[i] = 275;
                    break;

                case "Down":
                    inputs[i] = 274;
                    break;

                case "Up":
                    inputs[i] = 273;
                    break;

                default:
                    Debug.Log("Somehow all cases missed");
                    break;
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
            switch (inputs[i])
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
