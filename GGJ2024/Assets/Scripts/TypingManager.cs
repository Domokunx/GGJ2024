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
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite correct;
    [SerializeField] private Sprite mistake;

    private string qString;
    private string aString;

    private int questionIndex = 0;
    private int inputNum;

    private SpriteRenderer birdSR;
    // Start is called before the first frame update
    void Start()
    {
        if(question.Length != answer.Length)
        {
            Debug.LogError("Question and Answer number must be equale");
        }

        birdSR = bird.GetComponent<SpriteRenderer>();
        Output();
    }

    // Update is called once per frame
    void Update()
    {
        // check the letter one by one
        if(Input.GetKeyDown(aString[inputNum].ToString()))
        {
            Correct();

            Debug.Log(inputNum);
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

    void Correct()
    {
        inputNum++;
        if (inputNum >= answer[questionIndex].Length)
        {
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

        birdSR.sprite = correct;

        Debug.Log("correct");

    }

    void Failed()
    {
        // change textColor
        questionText.text = "<color=#FFFFFF>" + qString.Substring(0, inputNum) + "</color>" +
            "<color=#FF0000>" + qString.Substring(inputNum, 1) + "</color>" +
            qString.Substring(inputNum + 1);

        birdSR.sprite = mistake;

        Debug.Log("failed");
    }
}
