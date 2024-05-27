using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;
using UnityEngine;
using DialogueEditor;
using UnityEngine.UI;

public class npcTrigger : MonoBehaviour
{
    [SerializeField] private bool isFirstDialogue;
    [SerializeField] private NPCConversation dialogue;
    [SerializeField] private Text message;
    private bool isPlayerNearby;
    // Start is called before the first frame update
    void Start()
    {
        if (isFirstDialogue)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            message.text = "";
            ConversationManager.Instance.StartConversation(dialogue);
            isFirstDialogue = false;
            Debug.Log("dialog started");
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