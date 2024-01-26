using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BrainGame : MonoBehaviour
{
    [Header("Questions")]
    public Questions[] questions;

    [Space]

    [Header("Enemy Settings")]
    [SerializeField] private float solvingInterval = 2f;

    [Space]

    [Header("Object refs")]
    public TextMeshProUGUI playerQuestion;
    public TextMeshProUGUI enemyQuestion;
    public TMP_InputField inputField;

    #region Private Var
    private int playerIndex = 0;
    private int enemyIndex = 0;
    #endregion

    private void Awake()
    {
        inputField.Select();

        playerQuestion.text = questions[0].question;
        enemyQuestion.text = questions[0].question;
    }
    void Update()
    {
        
    }

    public void CheckInput()
    {
        if (string.IsNullOrWhiteSpace(inputField.text))
        {
            inputField.text = string.Empty;
            inputField.ActivateInputField();
            return;
        }

        if (CheckAnswer(playerIndex, int.Parse(inputField.text)))
        {
            playerQuestion.text = questions[++playerIndex].question;
        }
        else
        {
            // Play wrong answer audio or something
        }

        inputField.text = string.Empty;
        inputField.ActivateInputField();
    }
    private bool CheckAnswer(int questionIndex, int answer)
    {
        return questions[questionIndex].answer == answer;
    }
}
