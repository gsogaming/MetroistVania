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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            messagePanel.SetActive(true);
            messageText.text = "Fabric of reality is not as gruesome as you think here";
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
