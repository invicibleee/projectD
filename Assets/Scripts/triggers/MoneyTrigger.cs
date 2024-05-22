using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MoneyTrigger : MonoBehaviour
{
    private Player player;
    [SerializeField] private Text message;
    [SerializeField] private string text;
    private bool isPlayerNearby;
    private int money;
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        
    }

    public void GetCurrency(int i)
    {
        money = i;
    }
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PlayerManager.instance.AddEssences(money);
            gameObject.SetActive(false);
            money = 0;
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
