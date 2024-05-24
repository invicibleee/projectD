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

    private string saveKey = "LostStatusMoney";
    public bool status;
    private void Start()
    {
        Load();
        player = FindAnyObjectByType<Player>();
    }

    public void GetCurrency(int i)
    {
        money = i;
    }
    private void Update()
    {
        if (status && isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PlayerManager.instance.AddEssences(money);
            status = false;
            gameObject.SetActive(status);
            money = 0;
            message.text = "";
            Save();
        } else if (!status)
        {
            gameObject.SetActive(false);
        }
        Load();
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

    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }


    private void Load()
    {
        var data = SaveManager.Load<SaveData.LostStatusSave>(saveKey);
        status = data._status;
        gameObject.SetActive(data._status);
    }

    private SaveData.LostStatusSave GetData()
    {
        var data = new SaveData.LostStatusSave()
        {
            _status = status,
        };

        return data;
    }
}
