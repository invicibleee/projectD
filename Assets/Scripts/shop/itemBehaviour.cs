using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class itemBehaviour : MonoBehaviour
{
    private bool infuserOwned = false;
    private bool collarOwned = false;
    private bool dogQuestCompleted = false;
    private bool skeletonKey = false;
    private bool cannon = false;
    private bool lantern = false;
    private bool heart = false;
    private bool amulet = false;

    private string saveKey = "itemsSave";
    private string saveKey2 = "achivementsSave";
    private SaveData.AchivementsSave data2;
    public void Start()
    {
        Load();

        data2 = SaveManager.Load<SaveData.AchivementsSave>(saveKey2);
    }
    public void BuyInfuser(int amount)
    {
        if (PlayerManager.instance.essenceAmount >= amount)
        {
            InventoryPanelScript.instance.SetItemOwned(11);
            PlayerManager.instance.RemoveEssences(amount);
            Debug.Log("isBought" + ConversationManager.Instance.GetBool("isBought"));
            ConversationManager.Instance.SetBool("isEnough", true);
            ConversationManager.Instance.SetBool("isBought", true);
            infuserOwned = ConversationManager.Instance.GetBool("isBought");
            
        }
        else
        {
            ConversationManager.Instance.SetBool("isEnough", false);
        }
    }
    public void BuyKey()
    {
        InventoryPanelScript.instance.SetItemOwned(3);
    }

    public void BuyAmulet(int amount)
    {
        if (PlayerManager.instance.essenceAmount >= amount)
        {
            CharmsPanelScript.instance.SetCharmOwned(1);
            PlayerManager.instance.RemoveEssences(amount);
           
            ConversationManager.Instance.SetBool("isEnough", true);
        }
        else
        {
            ConversationManager.Instance.SetBool("isEnough", false);
        }
    }
    public void BuyHeart(int amount)
    {
        if (PlayerManager.instance.essenceAmount >= amount)
        {
            CharmsPanelScript.instance.SetCharmOwned(4);
            PlayerManager.instance.RemoveEssences(amount);

            ConversationManager.Instance.SetBool("isEnough", true);
        }
        else
        {
            ConversationManager.Instance.SetBool("isEnough", false);
        }
    }
    public void BuyCannon(int amount)
    {
        if (PlayerManager.instance.essenceAmount >= amount)
        {
            CharmsPanelScript.instance.SetCharmOwned(7);
            PlayerManager.instance.RemoveEssences(amount);

            ConversationManager.Instance.SetBool("isEnough", true);
            Debug.Log(ConversationManager.Instance.GetBool("isEnough"));
        }
        else
        {
            ConversationManager.Instance.SetBool("isEnough", false);
        }

    }
    public void BuyLantern(int amount)
    {
        if (PlayerManager.instance.essenceAmount >= amount)
        {
            InventoryPanelScript.instance.SetItemOwned(4);
            PlayerManager.instance.RemoveEssences(amount);

            ConversationManager.Instance.SetBool("isEnough", true);
        }
        else
        {
            ConversationManager.Instance.SetBool("isEnough", false);
        }
    }
    public void CheckInfuser()
    {
        infuserOwned = InventoryPanelScript.instance.items[11].isOwned;
        ConversationManager.Instance.SetBool("isBought", infuserOwned);

    }
    public void CheckCollar()
    {
        collarOwned = InventoryPanelScript.instance.items[6].isOwned;
        ConversationManager.Instance.SetBool("isCollarFound", collarOwned);

    }
    public void CheckKey()
    {
        skeletonKey = InventoryPanelScript.instance.items[3].isOwned;
        ConversationManager.Instance.SetBool("skeletonKeyOwned", skeletonKey);

    }
    public void CheckCannon()
    {
        cannon = CharmsPanelScript.instance.charms[7].isOwned;
        ConversationManager.Instance.SetBool("cannonOwned", cannon);

    }
    public void CheckHeart()
    {
        heart = CharmsPanelScript.instance.charms[4].isOwned;
        ConversationManager.Instance.SetBool("heartOwned", heart);

    }
    public void CheckAmulet()
    {
        amulet = CharmsPanelScript.instance.charms[1].isOwned;
        ConversationManager.Instance.SetBool("amuletOwned", amulet);

    }
    public void CheckLantern()
    {
        lantern = InventoryPanelScript.instance.items[4].isOwned;
        ConversationManager.Instance.SetBool("lanternOwned", lantern);

    }
    public void DogQuestCompleted(int amount)
    {
        if (!data2._isOwned[8])
        {
            MainMenu.instance.setAchivementOwned(8);
        }

        dogQuestCompleted = true;

        PlayerManager.instance.AddEssences(amount);
        Save();
    }

    public void CheckDogQuest()
    {
        Debug.Log("quest" + dogQuestCompleted);
        ConversationManager.Instance.SetBool("isQuestCompleted", dogQuestCompleted);
    }

    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }
    private void Load()
    {
        var data = SaveManager.Load<SaveData.NPCDialogues>(saveKey);
        dogQuestCompleted = data._isDogQuestCompleted;
    }
    private SaveData.NPCDialogues GetData()
    {
        var data = new SaveData.NPCDialogues()
        {
           _isDogQuestCompleted = dogQuestCompleted,
        };
        return data;
    }
}
