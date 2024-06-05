using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharmsCollision : MonoBehaviour
{
    [SerializeField] private int charmIndex;
    [SerializeField] private Text message;
    [SerializeField] private string text;

    private string saveKey = "PlayerCharms";
    private bool isPlayerNearby = false;
    private bool status;

    private void Start()
    {
        Load();
    }
    private void Update()
    {
        if (!status && isPlayerNearby && Input.GetKeyDown(InputSettings.Instance.GetKeyForAction(InputSettings.Instance.Button_use)))
        {
            CharmsPanelScript.instance.SetCharmOwned(charmIndex);
            StartCoroutine(DisplayMessageCoroutine("Found charm \"" + CharmsPanelScript.instance.charms[charmIndex].name + "\""));
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
    private IEnumerator DisplayMessageCoroutine(string messageText)
    {
        message.text = messageText;
        yield return new WaitForSeconds(1);
        message.text = "";
        gameObject.SetActive(false);
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.CharmsSave>(saveKey);
        status = data._isOwned[charmIndex];
    }
}
