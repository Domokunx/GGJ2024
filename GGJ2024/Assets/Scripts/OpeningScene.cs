using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OpeningScene : MonoBehaviour
{
    public TextMeshProUGUI mainTextElement;
    public TextMeshProUGUI promptTextElement;
    public Image imageElement;
    public GameObject continueButton;

    public string[] textArray;
    public Sprite[] imageArray;

    private int currentIndex = -1;
    private bool isTyping = false;

    public float typingSpeed = 0.05f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typing;
    void Start()
    {
        NextSet();
        continueButton.gameObject.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isTyping)
            {
                NextSet();
                promptTextElement.enabled = false;
            }
        }

        if (isTyping && !audioSource.isPlaying)
        {
            audioSource.clip = typing;
            audioSource.Play();
        }
        else if (!isTyping && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

    }

    void NextSet()
    {
        currentIndex++;

        if (currentIndex >= textArray.Length)
        {
            currentIndex = textArray.Length - 1;
            return;
        }

        StartCoroutine(TypingEffect());
        UpdateImage();
    }

    IEnumerator TypingEffect()
    {
        OutlineText();
        isTyping = true;

        if (mainTextElement != null && textArray.Length > currentIndex)
        {
            string currentText = textArray[currentIndex];
            mainTextElement.text = "";

            for (int i = 0; i <= currentText.Length; i++)
            {
                mainTextElement.text = currentText.Substring(0, i);
                yield return new WaitForSeconds(typingSpeed);
            }

            if (currentIndex == textArray.Length - 1)
            {
                continueButton.gameObject.SetActive(true);
            }

            isTyping = false;

            if (currentIndex < textArray.Length - 1)
            {
                ShowSpacePrompt();
            }
        }
    }

    void UpdateImage()
    {
        if (imageElement != null && imageArray.Length > currentIndex)
        {
            imageElement.sprite = imageArray[currentIndex];
        }
    }

    void ShowSpacePrompt()
    {
        promptTextElement.enabled = true;
    }

    void OutlineText()
    {
        Outline outline = mainTextElement.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = true;
            outline.effectColor = Color.red; 
            outline.effectDistance = new Vector2(2f, -2f);
        }
    }
}
