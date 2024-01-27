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
    }
}
