using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TypingManager : MonoBehaviour
{
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
    private int spriteIndex = 0;
    private int inputNum;

    private bool[] isMistake;
    private SpriteRenderer birdSR;


    // Start is called before the first frame update
    void Start()
    {
        if(question.Length != answer.Length)
        {
            Debug.LogError("Question and Answer number must be equale");
        }

        isMistake = new bool[question.Length];
        for(int i = 0; i < question.Length; i++)
        {
            isMistake[i] = false;
        }

        birdSR = bird.GetComponent<SpriteRenderer>();
        birdSR.sprite = correctSprite[spriteIndex];


        Output();
    }

    // Update is called once per frame
    void Update()
    {
        // check the letter one by one
        if(Input.GetKeyDown(aString[inputNum].ToString()))
        {
            Correct();
            //if(inputNum >= answer[questionIndex].Length)
            //{
            //    questionIndex++;
            //    Output();
            //}
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
            questionIndex = 0;
        }

        qString = question[questionIndex];
        aString = answer[questionIndex];

        questionText.text = qString;
    }

    private void Correct()
    {
        inputNum++;

        if (inputNum >= answer[questionIndex].Length)
        {
            ChangeSprite();
            questionIndex++;

            //if (questionIndex == 1)
            //{
            //    birdSR.sprite = r2Correct;
            //}
            //else if(questionIndex == 2)
            //{
            //    birdSR.sprite = r3Correct;
            //}
            //else if(questionIndex == 3)
            //{
            //    birdSR.sprite = last;
            //}

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
        isMistake[questionIndex] = true;


        // change textColor
        questionText.text = "<color=#FFFFFF>" + qString.Substring(0, inputNum) + "</color>" +
            "<color=#FF0000>" + qString.Substring(inputNum, 1) + "</color>" +
            qString.Substring(inputNum + 1);
    }

    private void ChangeSprite()
    {
        if (isMistake[questionIndex])
        {
            birdSR.sprite = mistakeSprite[spriteIndex];
        }
        else
        {
            spriteIndex++;

            birdSR.sprite = correctSprite[spriteIndex];
        }
    }
}
