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
    

    private void Awake()
    {
        messageText = GetComponent<TextMeshProUGUI>();       
        
    }

    private void Start()
    {
        textWriterSingle = TextWriter.AddWriter_Static(messageText, messageText.text, 0.1f,true,true);
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
            "But, it wasn't long before we realized that",
            "WE WERE NOT ALONE!"
                    };            
            string message = messageArray[currentIndex];
            textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.1f, true, true);
            currentIndex++;
            if (currentIndex >= messageArray.Length)
            {
                currentIndex = 0;
            }
        }
    }


}
