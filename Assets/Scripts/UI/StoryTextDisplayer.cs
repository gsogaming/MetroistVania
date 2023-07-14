using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryTextDisplayer : MonoBehaviour
{

    private TextMeshProUGUI messageText;
    private int currentIndex = 0;
    private TextWriter.TextWriterSingle textWriterSingle;

    [SerializeField] Button continueButton;


    private void Awake()
    {
        messageText = GetComponent<TextMeshProUGUI>();

    }

    private void Start()
    {
        textWriterSingle = TextWriter.AddWriter_Static(messageText, messageText.text, 0.1f, true, true);
        currentIndex = 1;
    }

    public void AddNextMessage()
    {
        if (textWriterSingle != null && textWriterSingle.IsActive())
        {
            textWriterSingle.WriteAllAndDestroy();
        }
        else
        {
            string[] messageArray = new string[]
                    {
             //story pieces
             //
             "The year 2022...",

            "The dawn of the advanced AI...",

            "It was in its baby steps, ",

            "It started as the name 'ChatGpt' ",

            "We thought it would be nothing but useful",

            "We simply didn't know that we were cooking our own destruction"
                    };
            string message = messageArray[currentIndex];
            textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.1f, true, true);
            currentIndex++;
            if (currentIndex >= messageArray.Length)
            {
                currentIndex = 0;
                continueButton.gameObject.SetActive(true);
            }
        }
    }


}
