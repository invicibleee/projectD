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
    private Player player;
    private bool isPlayerNearby;
    private int index;
    public float moveSpeed;
    private string saveKey = "NPCSave";
    // Start is called before the first frame update
    void Start()
    {
        player= FindAnyObjectByType<Player>();
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
        moveSpeed = player.moveSpeed;
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
            player.moveSpeed = 0;
            Save();
        }
        if (!ConversationManager.Instance.IsConversationActive)
        {
            player.moveSpeed = moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.CompareTag("Player"))
            {
                map.SetActive(false);
                message.text = "Press E to talk";
                isPlayerNearby = true;
                player.ZeroVelocity();
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