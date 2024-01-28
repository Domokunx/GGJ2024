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
    // Start is called before the first frame update
    void Start()
    {
        timerImage = GetComponent<Image>();
        transitionScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
