using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private GameObject transitionScreen;
    [SerializeField] private int timeLimit = 60;

    private Image timerImage;
    public float spendingTime = 0f;
    private bool isPaused = false;

    /*
    public static class CoroutineUtil
    {
        public static IEnumerator WaitForRealSeconds(float time)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup < start + time)
            {
                yield return null;
            }
        }
    }
    */

    void Start()
    {
        timerImage = GetComponent<Image>();
        transitionScreen.SetActive(false);
        isPaused = false;
        //StartCoroutine(PauseForSeconds());
    }

    void Update()
    {

        if (!MiniGameManager.miniGameStarted) return;

        if (!isPaused)
        {
            spendingTime += Time.deltaTime;
            float timer = spendingTime / timeLimit;

            timerImage.fillAmount = timer;

            if (spendingTime > timeLimit)
            {
                StartCoroutine(GameManager.BackToOutfitSelector(transitionScreen));
            }
        }
    }

    public void SetPaused(bool flag)
    {
        isPaused = flag;
    }

    public void SetTimeLimit(int time)
    {
        timeLimit = time;
        Debug.Log(timeLimit);

    }

    /*
    IEnumerator PauseForSeconds()
    {
        Time.timeScale = 0;
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(4f));
        Time.timeScale = 1;
        isPaused = false;
    }
    */
}
