using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] private TextMeshProUGUI playerQuestion;
    [SerializeField] private TextMeshProUGUI enemyQuestion;
    [SerializeField] private TextMeshProUGUI playerQuestionCounter;
    [SerializeField] private TextMeshProUGUI enemyQuestionCounter;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject transitionScreen;

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

        transitionScreen.SetActive(false);
    }
    void Update()
    {
        if (timeToNextSolve < Time.time)
        {
            EnemySolve();
        }
    }

    private void GameOver()
    {
                // Transition Scene tbc
                // Move back to outfitSelector
        StartCoroutine(GameManager.BackToOutfitSelector(transitionScreen));
    }
    private void EnemySolve()
    {
        timeToNextSolve += solvingInterval;

        if (enemyIndex + 1 ==  questions.Length)
        {
            GameOver();
            return;
        }

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
            if (playerIndex + 1 == questions.Length) 
            {
                GameOver(); // Might replace with a GameWin() to load different ending
                return;
            }

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
