using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BrainGame : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private string[] questions;
    [SerializeField] private int[] answers;

    [Space]

    [Header("Enemy Settings")]
    [SerializeField] private float solvingInterval = 2f;

    [Space]

    [Header("Object refs")]
    public TextMeshProUGUI playerQuestion;
    public TextMeshProUGUI enemyQuestion;
    public TextMeshProUGUI playerQuestionCounter;
    public TextMeshProUGUI enemyQuestionCounter;
    public TMP_InputField inputField;

    #region Private Var
    private int playerIndex = 0;
    private int enemyIndex = 0;

    private float timeToNextSolve;
    #endregion

    private void Awake()
    {
        inputField.Select();

        playerQuestion.text = questions[0];
        enemyQuestion.text = questions[0];

        playerQuestionCounter.text = "Questions Left: " + questions.Length.ToString();
        enemyQuestionCounter.text = "Questions Left: " + questions.Length.ToString();

        timeToNextSolve = solvingInterval;
    }
    void Update()
    {
        if (enemyIndex == questions.Length || playerIndex == questions.Length)
        {
            GameOver();
        }

        if (timeToNextSolve < Time.time)
        {
            EnemySolve();
        }
    }

    private void GameOver()
    {
        // I dunno do sth
        // Transition to next date?
    }
    private void EnemySolve()
    {
        timeToNextSolve += solvingInterval;
        enemyQuestion.text = questions[++enemyIndex];
        enemyQuestionCounter.text = "Questions Left: " + (questions.Length - enemyIndex).ToString();
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
            playerQuestion.text = questions[++playerIndex];
            playerQuestionCounter.text = "Questions Left: " + (questions.Length - playerIndex).ToString();
        }
        else
        {
            // Play wrong answer audio or something
            // Show cross
            // Cannot interact for about 0.5s
        }

        inputField.text = string.Empty;
        inputField.ActivateInputField();
    }
    private bool CheckAnswer(int questionIndex, int answer)
    {
        return answers[questionIndex] == answer;
    }
}
