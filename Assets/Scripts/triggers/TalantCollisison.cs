using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class TalantCollisison : MonoBehaviour
{
    private TalantsPanelScript talantsPanelScript;
    [SerializeField] private int talantIndex;
    [SerializeField] private Text message;
    [SerializeField] private string text;
    private string saveKey = "PlayerTalants";
    private string saveKey2 = "achivementsSave";
    private SaveData.AchivementsSave data2;

    private bool isPlayerNearby = false;
    private bool status;

    private void Start()
    {
        talantsPanelScript = TalantsPanelScript.instance;
        data2 = SaveManager.Load<SaveData.AchivementsSave>(saveKey2);
        Load();
    }
    private void Update()
    {
        if (!status && isPlayerNearby && Input.GetKeyDown(InputSettings.Instance.GetKeyForAction(InputSettings.Instance.Button_use)))
        {
            if (!data2._isOwned[6])
            {
                MainMenu.instance.setAchivementOwned(6);
            }
            talantsPanelScript.SetTalantOwned(talantIndex);
            StartCoroutine(DisplayMessageCoroutine("Found talant \"" + talantsPanelScript.talants[talantIndex].name + "\""));
           
        }
        else if (status)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            message.text = text;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            message.text = "";
        }
    }
    private void Load()
    {
        var data = SaveManager.Load<SaveData.TalantsSave>(saveKey);
        status = data._isOwned[talantIndex];
    }

    private IEnumerator DisplayMessageCoroutine(string messageText)
    {
        message.text = messageText;
        yield return new WaitForSeconds(1);
        message.text = "";
        gameObject.SetActive(false);
    }

}
