using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OpeningScene : MonoBehaviour
{
    public TextMeshProUGUI mainTextElement;
    public TextMeshProUGUI promptTextElement;
    public TextMeshProUGUI continueButtonText;
    public Image imageElement;
    public Button continueButton;

    public string[] textArray;
    public Sprite[] imageArray;

    private int currentIndex = -1;
    private bool isTyping = false;

    public float typingSpeed = 0.05f;

    //public AudioSource typingSource;

    void Start()
    {
        NextSet();
        continueButton.enabled = false;
        continueButtonText.enabled = false;

        /*
        if (typingSource == null)
        {
            typingSource = GetComponent<AudioSource>();
        }
        */
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

        /*
        if (isTyping && !typingSource.isPlaying)
        {
            typingSource.Play();
        }
        else if (!isTyping && typingSource.isPlaying)
        {
            typingSource.Stop();
        }
        */
    }

    void NextSet()
    {
        currentIndex++;

        if (currentIndex >= textArray.Length)
        {
            currentIndex = textArray.Length - 1;
            return;
        }

        // Start typing effect for the new set
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
                continueButton.enabled = true;
                continueButtonText.enabled = true;
            }

            isTyping = false;

            // Show space prompt after typing is complete (except for the last line)
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
            // Adjust the outline properties
            outline.enabled = true;
            outline.effectColor = Color.red; // Change the outline color to red
            outline.effectDistance = new Vector2(2f, -2f); // Adjust the outline size
        }
    }
}
