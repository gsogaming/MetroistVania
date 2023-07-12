using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InstructionsScript : MonoBehaviour
{
    public GameObject instructionsPanel;
    public TextMeshProUGUI instrucionsText;
    public GameObject endGameObject;
    public int enemiesDefeated = 0;

    private TextWriter.TextWriterSingle textWriterSingle;


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
        if (collision.gameObject.tag == "Player")
        {
            if (this.gameObject.name == "MovementInstructions")
            {
                instructionsPanel.SetActive(true);
                instrucionsText.text = "Use 'A' to move left and 'D' to move right";              
            }
            else if (this.gameObject.name == "JumpInstructions")
            {
                instrucionsText.text = "Press 'Spacebar' to Jump";
                
                instructionsPanel.SetActive(true);
            }
            else if (this.gameObject.name == "DoubleJumpInstructions")
            {
                instrucionsText.text = "Press '2X' Spacebar to perform a DoubleJump";
                instructionsPanel.SetActive(true);
            }
            else if (this.gameObject.name == "CombatInstructions")
            {
                instrucionsText.text = "Press 'Mouse1' to Attack";
                instructionsPanel.SetActive(true);
            }
            else if (this.gameObject.name == "CombatInstructions2")
            {
                instrucionsText.text = "You can bounce off of the enemies to kill them and gain a jump boost";
                instructionsPanel.SetActive(true);
            }
            else if (this.gameObject.name == "EndGameInstructions")
            {
                instrucionsText.text = "Kill all the enemies in this room to be able to beat the level";
                instructionsPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            instructionsPanel.SetActive(false);
        }

        if (this.gameObject.name == "EndGameInstructions")
        {
            if (collision.gameObject.tag == "Enemy")
            {                
                enemiesDefeated++;
                if (enemiesDefeated >= 5)
                {
                    instrucionsText.text = "Congratulations! Now Pick up your reward!";
                    endGameObject.SetActive(true);
                }               

            }

        }
    }
}
