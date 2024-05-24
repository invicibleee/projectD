using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollision : MonoBehaviour
{
    private InventoryPanelScript inventoryPanelScript;
    [SerializeField] private int itemIndex;
    [SerializeField] private Text message;
    [SerializeField] private string text;

    private string saveKey = "PlayerItems";
    private bool isPlayerNearby = false;
    private bool status;

    private void Start()
    {
        Load();
        inventoryPanelScript = InventoryPanelScript.instance;
    }
    private void Update()
    {
        if (!status && isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanelScript.SetItemOwned(itemIndex);
            gameObject.SetActive(false);
            message.text = "";
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
        status = data._isOwned[itemIndex];
    }
}
