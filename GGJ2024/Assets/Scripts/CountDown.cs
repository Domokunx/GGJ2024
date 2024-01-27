using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] private int count = 3;

    private bool countFinished;
    // Start is called before the first frame update
    void Start()
    {
        countFinished = false;
        StartCoroutine(Counting());
    }

    // Update is called once per frame
    void Update()
    {
        //if (countFinished) return;

        //if (count >= 0)
        //{
        //    count -= Time.deltaTime;

        //}
        //else if(count <= 0)
        //{
        //    count = 0;
        //    countDownText.text = "";
        //}
    }

    public bool GetCountDownFinished()
    {
        return countFinished;
    }

    IEnumerator Counting()
    {
        while (count >= 0)
        {
            yield return new WaitForSeconds(1f);
            count--;
            if (count == 0)
            {
                countDownText.text = "GO";
                countFinished = true;
            }
            else if (count == -1)
            {
                countDownText.text = "";
            }
            else countDownText.text = count.ToString("0");

        }
    }
}
