using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//this controls dialogue for all interactions int he game
public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;

    public GameObject player;

    private Queue<string> paragraphs = new Queue<string>();

    private bool conversationEnded;

    private string p;

    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        //if nothing in queue
        print("here");
        if (paragraphs.Count == 0)
        {
            if (!conversationEnded)
            {
                //start convo
                StartConversation(dialogueText);
            }
            else
            {
                //end convo and leave
                EndConversation();
                return;
            }
        }

        //if there is something in the queue
        p = paragraphs.Dequeue();

        //update converstaion text
        NPCDialogueText.text = p;

        if (paragraphs.Count == 0)
        {
            conversationEnded = true;
        }
    }

    private void StartConversation(DialogueText dialogueText)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        // //stop the player from moving
        player.GetComponent<PlayerMove>().enabled = false;

        //update the speaker name
        NPCNameText.text = dialogueText.speakerName;

        //add all dialogue text into queue
        for (int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
    }

    private void EndConversation()
    {
        //clear the queue
        paragraphs.Clear();

        //return bool to false
        conversationEnded = false;

        // //allow the player to move again
        player.GetComponent<PlayerMove>().enabled = true;

        //deactivate gameObject
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
