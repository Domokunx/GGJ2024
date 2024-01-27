using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private int timeLimit = 60;

    private Image timerImage;
    private float spendingTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timerImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        spendingTime += Time.deltaTime;
        float timer = spendingTime / timeLimit;

        timerImage.fillAmount = timer;
    }
}
