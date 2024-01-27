using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickYourDateManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (GameManager.rizzed[i])
            {
                buttons[i].interactable = true;
            }
        }
    }


}
