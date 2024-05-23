using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player player;
    public static PlayerManager instance;
    public int essenceAmount;
    private string saveKey = "playerMoneySave";

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        } else
        {
            instance = this;
        }
    }
    private void Start()
    {
        Load();
    }

    public void AddEssences(int amount)
    {
        essenceAmount += amount;
        Save();
    }

    // Method to remove essences from the player
    public void RemoveEssences(int amount)
    {

        essenceAmount -= amount;
        Save();
    }

    // Method to get the current essence amount
    public int GetEssenceAmount()
    {
        Save();
        return essenceAmount;
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());

    }


    private void Load()
    {
        var data = SaveManager.Load<SaveData.MoneyPlayer>(saveKey);
        essenceAmount = data._money;

    }

    private SaveData.MoneyPlayer GetData()
    {
        var data = new SaveData.MoneyPlayer()
        {
            _money = essenceAmount,

        };

        return data;

    }
}
