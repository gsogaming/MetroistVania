using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject messagePanel; 
    [SerializeField] TextMeshProUGUI messageText;
    private TextWriter.TextWriterSingle textWriterSingle;    
    [Range(0f, 3f)] // This sets the range of the slider to be between 0 and 3    
    [SerializeField] float writeSpeed;
    string storedText;

    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        textWriterSingle = TextWriter.AddWriter_Static(messageText, messageText.text, writeSpeed, true, true);
        storedText = messageText.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (!textWriterSingle.IsActive() && this.gameObject.tag == "Prop")
        {
                                 
            textWriterSingle = TextWriter.AddWriter_Static(messageText, storedText, writeSpeed, true, true);
            
        }
        else
        {
            return;
        }
    }  

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            messagePanel.SetActive(true);            
            textWriterSingle = TextWriter.AddWriter_Static(messageText, storedText, 0.1f, true, true);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            messagePanel.SetActive(false);            
        }
    }
}
