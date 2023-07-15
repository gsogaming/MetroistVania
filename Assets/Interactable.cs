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

    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        textWriterSingle = TextWriter.AddWriter_Static(messageText, messageText.text, 0.1f, true, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!textWriterSingle.IsActive() && this.gameObject.tag == "Prop")
        {
            textWriterSingle = TextWriter.AddWriter_Static(messageText, "BLA BLA BLA BLA BLA BLA BLA BLA", 0.1f, true, true);
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
            messageText.text = "Fabric of Reality...";
            textWriterSingle = TextWriter.AddWriter_Static(messageText, messageText.text, 0.1f, true, true);
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
