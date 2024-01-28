using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPopup : MonoBehaviour
{
    public Canvas canvas;
    public bool a = false;
    public void PopUp()
    {
        if (a == false)
        {
            a = true;
            canvas.enabled = true;
        }
        else if (a == true)
        {
            a = false;
            canvas.enabled = false;
        }
    }
}
