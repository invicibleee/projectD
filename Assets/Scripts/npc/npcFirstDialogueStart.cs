using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcFirstDialogueStart : MonoBehaviour
{
    [SerializeField] private bool isFirstDialogue;
    [SerializeField] private NPCConversation dialogue;
    [SerializeField] private NPCConversation dialogue2;
    [SerializeField] private Text message;
    private bool isPlayerNearby;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (isFirstDialogue)
            {
                message.text = "";
                ConversationManager.Instance.StartConversation(dialogue);
                isFirstDialogue = false;
            }
            else
            {
                ConversationManager.Instance.StartConversation(dialogue2);
             
            }
         
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            message.text = "Press E to talk";
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            message.text = "";
            isPlayerNearby = false;
        }
    }
}
