using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitSelectorManager : MonoBehaviour
{
    [SerializeField] private Button infoBtn;
    [SerializeField] private Button closeBtn;
    [SerializeField] private GameObject infoImg;
    [SerializeField] private Button nextBtn;

    // Start is called before the first frame update
    void Start()
    {
        infoImg.SetActive(false);
    }

    public void ShowInfo()
    {
        infoImg.SetActive(true);
    }

    public void HideInfo()
    {
        infoImg.SetActive(false);
    }

    public void DisableSpam()
    {
        nextBtn.interactable = false;
    }
}
