using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrainGame : MonoBehaviour
{
    [Header("Difficulty Settings")]
    [SerializeField] private int questionCount;

    [Header("Enemy Settings")]
    [SerializeField] private float solvingInterval = 2f;

    [Space]

    [Header("Object refs")]
    [SerializeField] private TextMeshProUGUI playerQuestion;
    [SerializeField] private TextMeshProUGUI enemyQuestion;
    [SerializeField] private TextMeshProUGUI playerQuestionCounter;
    [SerializeField] private TextMeshProUGUI enemyQuestionCounter;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject winTransitionScreen;
    [SerializeField] private GameObject loseTransitionScreen;

    #region Private Var
    private int playerIndex = 0;
    private int enemyIndex = 0;

    private float timeToNextSolve;

    private readonly string[] questions = new string[]
    {
        "2 + 2",
        "4 + 4",
        "8 + 8",
        "8 / 2",
        "7 + 4",
        "2 - 1",
        "0 + 0",
        "6 + 5",
        "8 / 4",
        "4 * 7",
        "1 * 2",
        "4 * 3",
        "9 - 8",
        "10 + 1",
        "3 * 7",
        "4 + 12",
        "8 - 2",
        "14 / 2",
        "18 / 3",
        "6 + 1",
        "2 - 2",
        "3 + 4",
        "8 * 1",
        "9 + 9"

    };
    private readonly string[] answers = new string[]
    {
        "4",
        "8",
        "16",
        "4",
        "11",
        "1",
        "0 ",
        "11",
        "2",
        "28",
        "2",
        "12",
        "1",
        "11",
        "21",
        "16",
        "6",
        "7",
        "6",
        "7",
        "0",
        "7",
        "8",
        "18"

    };

    private int[] questionIndices;
    #endregion

    private void Awake()
    {
        inputField.Select();

        questionIndices = new int[questionCount];
        for (int i = 0; i < questionCount; i++)
        {
            questionIndices[i] = Random.Range(0, questions.Length);
        }

        playerQuestionCounter.text = "Questions Left: " + questionCount.ToString();
        enemyQuestionCounter.text = "Questions Left: " + questionCount.ToString();

        playerQuestion.text = questions[questionIndices[0]];
        enemyQuestion.text = questions[questionIndices[0]];

        timeToNextSolve = solvingInterval;

        winTransitionScreen.SetActive(false);
    }
    void Update()
    {
        if (!MiniGameManager.miniGameStarted) return;

        if (timeToNextSolve < Time.timeSinceLevelLoad)
        {
            EnemySolve();
        }
    }

    private void EnemySolve()
    {
        timeToNextSolve += solvingInterval;

        if (enemyIndex + 1  ==  questionCount)
        {
            StartCoroutine(GameManager.BackToOutfitSelector(loseTransitionScreen));
            return;
        }

        enemyQuestion.text = questions[questionIndices[++enemyIndex]];
        enemyQuestionCounter.text = "Questions Left: " + (questionCount - enemyIndex).ToString();
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
            if (playerIndex + 1 == questionCount) 
            {
                GameManager.rizzed[3] = true;
                StartCoroutine(GameManager.BackToOutfitSelector(winTransitionScreen));
                timeToNextSolve += 999999f;
                return;
            }

            playerQuestion.text = questions[questionIndices[++playerIndex]];
            playerQuestionCounter.text = "Questions Left: " + (questionCount - playerIndex).ToString();
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
        return int.Parse(answers[questionIndices[questionIndex]]) == answer;
    }
}
