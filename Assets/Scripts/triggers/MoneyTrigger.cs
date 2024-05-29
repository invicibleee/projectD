using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MoneyTrigger : MonoBehaviour
{
    private Player player;
    [SerializeField] private Text message;
    [SerializeField] private string text;
    private bool isPlayerNearby;
    private int money;
    private int index;

    private string saveKey = "LostStatusMoney";
    private string saveKey2 = "LostMoney";

    public bool status;
    private void Start()
    {
        Load();
        Load2();
        if(index != SceneManager.GetActiveScene().buildIndex)
        {
            gameObject.SetActive(false);
        }
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
    private void Load2()
    {
        var data = SaveManager.Load<SaveData.LostMoneySave>(saveKey2);
        index = data._sceneIndex;
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
