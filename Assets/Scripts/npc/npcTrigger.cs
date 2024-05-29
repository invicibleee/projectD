using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DialogueEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class npcTrigger : MonoBehaviour
{
    [SerializeField] private bool isFirstDialogue;
    [SerializeField] private NPCConversation dialogue;
    [SerializeField] private Text message;
    [SerializeField] private GameObject map;
    private bool isPlayerNearby;
    private int index;

    private string saveKey = "NPCSave";
    // Start is called before the first frame update
    void Start()
    {
        Load();
        index = SceneManager.GetActiveScene().buildIndex;

        switch (index)
        {
            case 2:
                if (isFirstDialogue)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case 3:
                if (!isFirstDialogue)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
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
            Save();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.CompareTag("Player"))
            {
                map.SetActive(false);
                message.text = "Press E to talk";
                isPlayerNearby = true;
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            map.SetActive(true);
            message.text = "";
            isPlayerNearby = false;
        }
    }


    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }
    private void Load()
    {
        var data = SaveManager.Load<SaveData.NPCDialogues>(saveKey);
        isFirstDialogue = data._isFirstPilgrimDialogue;
    }
    private SaveData.NPCDialogues GetData()
    {
        var data = new SaveData.NPCDialogues()
        {
            _isFirstPilgrimDialogue = isFirstDialogue,
        };
        return data;
    }

}