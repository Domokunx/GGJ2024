using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickYourDateManager : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles;

    private bool[] selected;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Toggle btn in toggles)
        {
            btn.interactable = false;
        }

        for (int i = 0; i < toggles.Length; i++)
        {
            if (GameManager.rizzed[i])
            {
                toggles[i].interactable = true;
            }
        }
    }


}
