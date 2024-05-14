using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player player;
    public static PlayerManager instance;
    public int essenceAmount;

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
    public void AddEssences(int amount)
    {
        essenceAmount += amount;
    }

    // Method to remove essences from the player
    public void RemoveEssences(int amount)
    {
        essenceAmount -= amount;
    }

    // Method to get the current essence amount
    public int GetEssenceAmount()
    {
        return essenceAmount;
    }

}
