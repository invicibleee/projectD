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

    private bool isPlayerNearby = false;

    private void Start()
    {
        inventoryPanelScript = InventoryPanelScript.Instance;
    }
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanelScript.SetItemOwned(itemIndex);
            Destroy(gameObject);
            message.text = "";
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
}
