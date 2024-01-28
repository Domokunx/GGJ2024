using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitSelectorManager : MonoBehaviour
{
    [SerializeField] private Button infoBtn;
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private GameObject[] infoCards;
    [SerializeField] private Transform infoCardLocation;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip click;

    private GameObject info;
    private void Awake()
    {
        info = Instantiate(infoCards[GameManager.nextDate - 3], infoCardLocation);
        HideInfo();
    }
    public void ShowInfo()
    {
        info.SetActive(true);
        closeBtn.gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        info.SetActive(false);
        closeBtn.gameObject.SetActive(false);

    }

    public void DisableSpam()
    {
        nextBtn.interactable = false;
    }
}
