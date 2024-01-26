using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TypingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;

    [SerializeField] private string[] question;
    [SerializeField] private string[] answer;

    private string qString;
    private string aString;

    private int questionIndex = 0;
    private int inputNum;
    // Start is called before the first frame update
    void Start()
    {
        Output();
    }

    // Update is called once per frame
    void Update()
    {
        // check the letter one by one
        if(Input.GetKeyDown(aString[inputNum].ToString())) 
        {
            Correct();

            if(inputNum >= question[questionIndex].Length)
            {
                questionIndex++;
                Output();
            }
        }   
        else if(Input.anyKeyDown)
        {
            Failed();
        }
    }

    void Output()
    {
        inputNum = 0;

        if(question.Length < questionIndex)
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
        questionText.text = "<color=#FFFFFF>" + qString.Substring(0, inputNum) +"</color>"+ qString.Substring(inputNum);

        Debug.Log("correct");
    }

    void Failed()
    {
        questionText.text = "<color=#FFFFFF>" + qString.Substring(0, inputNum) + "</color>" +
            "<color=#FFFFFF>" + qString.Substring(inputNum, 1) + "</color>" + qString.Substring(inputNum + 1);

        Debug.Log("failed");
    }
}
