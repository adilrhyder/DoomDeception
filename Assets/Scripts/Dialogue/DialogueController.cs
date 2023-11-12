using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//this controls dialogue for all interactions int he game
public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private float typeSpeed = 10;

    public GameObject player;

    private Queue<string> paragraphs = new Queue<string>();

    private bool conversationEnded;
    private bool isTyping;

    private string p;

    private Coroutine typeDialogueCoroutine;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

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
            else if (conversationEnded && !isTyping)
            {
                //end convo and leave
                EndConversation();
                return;
            }
        }

        //if there is something in the queue
        if (!isTyping)
        {
            p = paragraphs.Dequeue();
            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }
        //else conversation is being typed out
        else
        {
            FinishParagraphEarly();
        }

        //update converstaion text (old way of doing it -- allows for spillover)
        // NPCDialogueText.text = p;

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

        //stop the player from moving
        player.GetComponent<PlayerMove>().enabled = false;
        //stop the player from using their gun
        player.GetComponentInChildren<Gun>().enabled = false;

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

        //allow the player to move again
        player.GetComponent<PlayerMove>().enabled = true;
        //allow the player to use their gun again
        player.GetComponentInChildren<Gun>().enabled = true;



        //deactivate gameObject
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    //this function sets an alpha character that moves along an already printed line
    //this has the benefit of not causing a word to be spilt over to the next line if 
    //it does not fit. It's neater since all text is already printed and in its right place. 
    private IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;

        NPCDialogueText.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            NPCDialogueText.text = originalText;

            //moves alpha character forward
            displayedText = NPCDialogueText.text.Insert(alphaIndex, HTML_ALPHA);
            NPCDialogueText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME/typeSpeed);
        }

        isTyping = false;
    }

    private void FinishParagraphEarly()
    {
        //stop the coroutine
        StopCoroutine(typeDialogueCoroutine);

        //finish displaying text
        NPCDialogueText.text = p;

        //update isTyping bool
        isTyping = false;
    }
}
