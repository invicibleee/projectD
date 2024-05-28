using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class npcFirstDialogueStart : MonoBehaviour
{
    [SerializeField] private bool isFirstDialogue;
    [SerializeField] private NPCConversation dialogue;
    [SerializeField] private NPCConversation dialogue2;
    [SerializeField] private Text message;
    [SerializeField] private string typeOfNpc;
    [SerializeField] private GameObject npcIcon;
    [SerializeField] private GameObject map;
    private bool isPlayerNearby;
    private bool shopOpened;

    private string saveKey = "NPCSave2";
    private string saveKey2 = "IconsSave";
    private string saveKey3 = "NPCSave3";

    // Start is called before the first frame update
    void Start()
    {
        Load2();
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            Load();
        } else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            Load3();
        }
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
                if(typeOfNpc == "Mushroom")
                {
                    Save();
                } else if (typeOfNpc == "Snake")
                {
                    Save3();
                }
            }
            else
            {
                if(typeOfNpc == "Knight")
                {
                    ConversationManager.Instance.StartConversation(dialogue);
                }
                else
                {
                    ConversationManager.Instance.StartConversation(dialogue2);
                }
              
             
            }
         
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            message.text = "Press E to talk";
            isPlayerNearby = true;

            if (typeOfNpc == "Snake")
            {
                map.SetActive(false);
                npcIcon.SetActive(true);
                shopOpened = true;
                Save2();
            }
        }
     
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            message.text = "";
            isPlayerNearby = false;
        }
        if (typeOfNpc == "Snake")
        {
            map.SetActive(true);
        }
    }

    public void Save2()
    {
        SaveManager.Save(saveKey2, GetData2());
    }
    private void Load2()
    {
        var data = SaveManager.Load<SaveData.IconsSave>(saveKey2);
        shopOpened = data._isSnakeVisited;
        npcIcon.SetActive(shopOpened);
    }
    private SaveData.IconsSave GetData2()
    {
        var data = new SaveData.IconsSave()
        {
            _isSnakeVisited = shopOpened,
        };
        return data;
    }

    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }
    private void Load()
    {
        var data = SaveManager.Load<SaveData.NPCDialogues>(saveKey);
        isFirstDialogue = data._isFirstMushroomDialogue;
    }
    private SaveData.NPCDialogues GetData()
    {
        var data = new SaveData.NPCDialogues()
        {
            _isFirstMushroomDialogue = isFirstDialogue,
        };
        return data;
    }


    public void Save3()
    {
        SaveManager.Save(saveKey3, GetData3());
    }
    private void Load3()
    {
        var data = SaveManager.Load<SaveData.NPCDialogues>(saveKey3);
        isFirstDialogue = data._isFirstSnakeDialogue;
    }
    private SaveData.NPCDialogues GetData3()
    {
        var data = new SaveData.NPCDialogues()
        {
            _isFirstSnakeDialogue = isFirstDialogue,
        };
        return data;
    }

}
