using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private CountDown countDown;

    public static bool miniGameStarted;

    // Start is called before the first frame update
    void Start()
    {
        countDown = this.GetComponent<CountDown>();

        miniGameStarted = false;
        StartCoroutine(countDown.Counting());
    }

    // Update is called once per frame
    void Update()
    {
        if (!countDown.GetCountDownFinished()) return;

        if (!miniGameStarted)
        {
            miniGameStarted = true;
        }
    }

    public bool GetMiniGameStarted()
    {
        return miniGameStarted;
    }
}
