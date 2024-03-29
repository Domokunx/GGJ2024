using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitSelectorManager : MonoBehaviour
{
    [SerializeField] private Button infoBtn;
    [SerializeField] private Button closeBtn;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button nextBtn;
    [SerializeField] private GameObject[] infoCards;
    [SerializeField] private Transform infoCardLocation;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip click;

    private GameObject info;
    private void Awake()
    {
        info = Instantiate(infoCards[GameManager.nextDate - 3], infoCardLocation);
        ShowInfo();
    }
    public void ShowInfo()
    {
        audioSource.clip = click;
        audioSource.Play();
        info.SetActive(true);
        closeBtn.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        audioSource.clip = click;
        audioSource.Play();
        info.SetActive(false);
        closeBtn.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);

    }

    public void DisableSpam()
    {
        nextBtn.interactable = false;
    }
}
