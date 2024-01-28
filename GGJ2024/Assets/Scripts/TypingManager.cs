using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TypingManager : MonoBehaviour
{
    [SerializeField] private GameObject transitionScreen;
    [SerializeField] private GameTimer gameTimer;

    [Header("Question Setting")]
    [SerializeField] private TextMeshProUGUI questionText;

    [SerializeField] private string[] question;
    [SerializeField] private string[] answer;

    [Header("Bird Sprite")]
    [SerializeField] private GameObject bird;
    [SerializeField] private Sprite[] correctSprite = new Sprite[4];

    [Space]
    [SerializeField] private Sprite[] mistakeSprite = new Sprite[3];

    private string qString;
    private string aString;

    private int questionIndex = 0;
    private int spriteIndex = 0;    // this can be score
    private int inputNum;

    private bool isMistake;
    private SpriteRenderer birdSR;

    private bool finished;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip restaurantKey, correct, wrong;


    // Start is called before the first frame update
    void Start()
    {
        if(question.Length != answer.Length)
        {
            Debug.LogError("Question and Answer number must be equale");
        }

        transitionScreen.SetActive(false);

        isMistake = false;

        birdSR = bird.GetComponent<SpriteRenderer>();
        birdSR.sprite = correctSprite[spriteIndex];

        finished = false;

        gameTimer.SetTimeLimit(
            DifficultySettings.instance.typingTimer[DifficultySettings.instance.difficulty]);

        Output();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MiniGameManager.miniGameStarted) return;

        if (finished)
        {
            FinishGame();
            return;
        }
        // check the letter one by one
        if(Input.GetKeyDown(aString[inputNum].ToString()))
        {
            Correct();
        }   
        else if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.LeftShift))
        {
            Failed();
        }
    }

    void Output()
    {
        inputNum = 0;

        // just avoid error
        if(question.Length <= questionIndex)
        {
            finished = true;
            return;
        }

        qString = question[questionIndex];
        aString = answer[questionIndex];

        questionText.text = qString;
    }

    private void Correct()
    {
        audioSource.clip = restaurantKey;
        audioSource.Play();

        inputNum++;

        if (inputNum >= answer[questionIndex].Length)
        {
            audioSource.clip = correct;
            audioSource.Play();

            ChangeSprite();
            questionIndex++;
            Output();
            return;
        }

        if (aString[inputNum].ToString() == " ")
        {
            inputNum++;
        }

        // change textColor
        questionText.text = "<color=#FFFFFF>" + qString.Substring(0, inputNum) +"</color>"+
             "<color=#FFFF00>" + qString.Substring(inputNum, 1) + "</color>" +
             qString.Substring(inputNum + 1);

    }

    private void Failed()
    {
        //isMistake = true;
        audioSource.clip = wrong;
        audioSource.Play();

        // change textColor
        questionText.text = "<color=#FFFFFF>" + qString.Substring(0, inputNum) + "</color>" +
            "<color=#FF0000>" + qString.Substring(inputNum, 1) + "</color>" +
            qString.Substring(inputNum + 1);
    }

    private void ChangeSprite()
    {
        if (isMistake)
        {
            birdSR.sprite = mistakeSprite[spriteIndex];
        }
        else
        {
            spriteIndex++;

            birdSR.sprite = correctSprite[spriteIndex];
        }
    }

    private void FinishGame()
    {
        gameTimer.SetPaused(true);

        GameManager.rizzed[0] = true;
        StartCoroutine(GameManager.BackToOutfitSelector(transitionScreen));
    }
}
