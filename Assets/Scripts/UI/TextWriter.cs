using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;
    private List<TextWriterSingle> textWriterSingleList;

    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }
    public static TextWriterSingle AddWriter_Static(TextMeshProUGUI textToEdit, string textToWrite, float timePerCharacter, bool invisibleCharacters,bool removeWriterBeforeAdd)
    {
        if (removeWriterBeforeAdd)
        {
            instance.RemoveWriter(textToEdit);
        }
        return instance.AddWriter(textToEdit,textToWrite,timePerCharacter,invisibleCharacters);
    }
    
    private TextWriterSingle AddWriter(TextMeshProUGUI textToEdit, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(textToEdit, textToWrite, timePerCharacter, invisibleCharacters);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriter_Static(TextMeshProUGUI textToEdit)
    {
        instance.RemoveWriter(textToEdit);

    }

    private void RemoveWriter(TextMeshProUGUI textToEdit)
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            if (textWriterSingleList[i].GettheText() == textToEdit)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }

    }

    private void Update()
    {
        Debug.Log(textWriterSingleList.Count);
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }

        }        
    }

    public class TextWriterSingle
    {
        private TextMeshProUGUI textToEdit;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;
        

        public TextWriterSingle(TextMeshProUGUI textToEdit, string textToWrite, float timePerCharacter, bool invisibleCharacters)
        {
            
            this.textToEdit = textToEdit;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            characterIndex = 0;
        }
        public bool Update()
        {

            timer -= Time.deltaTime;
            while (timer < 0f)
            {
                //Display next character
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }

                textToEdit.text = text;

                if (characterIndex >= textToWrite.Length)
                {
                    //Entire String displayed                    
                    return true;
                }
            }

            return false;

        }

        public TextMeshProUGUI GettheText()
        {
            return textToEdit;
        }

        public bool IsActive()
        {
            return characterIndex < textToWrite.Length;
        }

        public void WriteAllAndDestroy()
        {
            //write the whole text and destroy the textwriter.
            textToEdit.text = textToWrite;
            characterIndex = textToWrite.Length;            
            TextWriter.RemoveWriter_Static(textToEdit);
        }
    }
}
