using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public struct CharmInfo
{
    public Image image;
    public int cost;
    public bool isOwned;
    public bool isUnique;
    public bool isEquiped;
    public string description;
    public string name;
}

public class CharmsPanelScript : MonoBehaviour
{
    PauseMenuScript pauseMenuScript;
    [SerializeField] private Charm[] charmsArray;
    [SerializeField] private CharmInfo[] charms;
    [SerializeField] private CharmInfo[] equippedCharms = new CharmInfo[3];
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text Prompt;
    [SerializeField] private GameObject confirm;

    private int currentCharm;
    private int selectedCharmIndex = -1;

    [SerializeField] private Image Image_one;
    [SerializeField] private Image Image_two;
    [SerializeField] private Image Image_three;
    [SerializeField] private Sprite basic;
    [SerializeField] private Sprite none;
    private int lastClickedIndex = -1;

    private string saveKey = "PlayerCharms";

    private void Awake()
    {
        charmsArray = FindObjectsOfType<Charm>();
    }
    void Start()
    {
        Load();
        pauseMenuScript = GetComponent<PauseMenuScript>();
        currentCharm = 1;
        descriptionText.text = "select skill";
        Prompt.text = "";
        nameText.text = "";
        UpdateCharmImages();

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            descriptionText.text = "select skill";
            Prompt.text = "";
            nameText.text = "";
            UpdateCharmImages();
        }
    }


    public void OnEquipedCharmImageClick(int charmIndex)
    {
        selectedCharmIndex = GetEquipedCharmInList(charmIndex);
        if (selectedCharmIndex != -1)
        {
            Prompt.text = "unequip charm?";
            UpdateDescriptionText(selectedCharmIndex);
        }
    }

    public void OnCharmImageClick(int charmIndex)
    {
        Prompt.text = "";
        confirm.SetActive(true);
        UpdateDescriptionText(charmIndex);

        if (selectedCharmIndex != -1)
        {
            charms[selectedCharmIndex].image.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        charms[charmIndex].image.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        selectedCharmIndex = charmIndex;
    }

    private void UpdateDescriptionText(int charmIndex)
    {
        if (charmIndex >= 0 && charmIndex < charms.Length)
        {
            nameText.text = charms[charmIndex].name;
            descriptionText.text = charms[charmIndex].description;
        }
        else
        {
            Debug.LogError("Invalid description index: " + charmIndex);
        }
    }

    private void UpdateCharmImages()
    {
        for (int i = 0; i < charms.Length; i++)
        {
            // Set image color based on whether the charm is purchased or not
            charms[i].image.color = charms[i].isOwned ? Color.white : Color.black;
        }
    }

    public void PurchaseCharm()
    {
        int charmIndex = selectedCharmIndex;
        if (charmIndex >= 0 && charmIndex < charms.Length)
        {
            CharmInfo currentCharm = charms[charmIndex]; 
            if (currentCharm.isOwned)
            {
                if (!currentCharm.isEquiped)
                {
                    EquipCharm(charmIndex);
                }
                else UnequipeCharm(charmIndex);
            }
            else
            {
                if (currentCharm.cost == 0 || pauseMenuScript.GetEssenceAmount(0) < currentCharm.cost)
                {
                    Prompt.text = "Charm unowned";
                    descriptionText.text = "This charm is not available for purchase.";
                }
                else
                {
                    pauseMenuScript.UseEssenceAmount(currentCharm.cost);
                    Prompt.text = "charm purshcased";
                    charms[charmIndex].isOwned = true;
                    Debug.Log(currentCharm.isOwned);
                    UpdateCharmImages();
                }
            }
        }
        else
        {
            descriptionText.text = "You have not selected a charm.";
        }

        UpdateCharmImages();
        Save();
    }

    public void LastClicked(int index)
    {
        lastClickedIndex = index;
    }


    public void SetCharmOwned(int charmIndex)
    {
        CharmInfo[] updatedCharms = charms;

        if (charmIndex < updatedCharms.Length)
        {
            updatedCharms[charmIndex].isOwned = true;
            charms = updatedCharms;

            // Add logic to handle the charm discovery
            if (true)
            {
                Debug.Log("You found Charm Index: " + charmIndex);
            }
        }
        Save();
    }
    private void UnequipeCharm(int charmIndex)
    {
        selectedCharmIndex = -1;
        nameText.text = "";
        descriptionText.text = "select skill";
        Prompt.text = "charm unequiped";
        CharmInfo currentCharm = charms[charmIndex];
        charms[charmIndex].isEquiped = false;

        for (int i = 0; i < equippedCharms.Length; i++)
        {
            if (currentCharm.description == equippedCharms[i].description)
            {
                equippedCharms[i].image = null;
                equippedCharms[i].description = "";
                equippedCharms[i].isEquiped = false;
                equippedCharms[i].isUnique = false;
                equippedCharms[i].isOwned = false;
                equippedCharms[i].cost = 0;
                DeleteColorForSlot(i);
            }
        }
        if (!charms[charmIndex].isEquiped && charms != null)
        {
            Debug.Log("deactivated " + charmsArray[charmIndex]);
            charmsArray[charmIndex].DeactivateEffect();
        }

    }
    private void EquipCharm(int charmIndex)
    {
        if (charms[charmIndex].isUnique)
        {
            int equippedUniqueIndex = FindIndexOfEquippedUniqueCharm();
            int previousIndex = FindIndexOfEquippedUniqueCharmInLIst();
            if (equippedUniqueIndex != -1)
            {
                Debug.Log("found unique ");
                if (!equippedCharms[equippedUniqueIndex].Equals(charms[charmIndex]))
                {
                    charms[previousIndex].isEquiped = false;
                    equippedCharms[equippedUniqueIndex] = charms[charmIndex];

                    charms[charmIndex].isEquiped = true;
                    SetColorForSlot(equippedUniqueIndex);
                    UpdateDescription(equippedUniqueIndex);
                }
                return;
            }
        }
        int freeSlot = FindFreeSlot();

        if (freeSlot != -1)
        {
            equippedCharms[freeSlot] = charms[charmIndex];

            charms[charmIndex].isEquiped = true;
            SetColorForSlot(freeSlot);
            UpdateDescription(freeSlot);

        }
        else
        {
            Prompt.text = "No slots available";
        }

        if (charms[charmIndex].isEquiped)
        {
            Prompt.text = "charm equiped";
            Debug.Log("activated" + charmsArray[charmIndex]);
            charmsArray[charmIndex].ActivateEffect();
        }
    }
    private int FindIndexOfEquippedUniqueCharm()
    {
        for (int i = 0; i < equippedCharms.Length; i++)
        {
            if (equippedCharms[i].isUnique)
            {
                return i;
            }
        }

        return -1;
    }
    private int FindIndexOfEquippedUniqueCharmInLIst()
    {
        for (int i = 0; i < equippedCharms.Length; i++)
        {
            if (equippedCharms[i].isUnique)
            {
                for (int j = 0; j < charms.Length; j++)
                {
                    if (equippedCharms[i].description == charms[j].description)
                    {
                        return j;
                    }
                }
            }
        }

        return -1;
    }

    private int GetEquipedCharmInList(int index)
    {
        for (int i = 0; i < charms.Length; i++)
        {
            if (equippedCharms[index].description == charms[i].description)
            {
                Debug.Log(i);
                return i;
            }
        }
        return -1;
    }
    private int FindFreeSlot()
    {
        //find first free slot
        for (int i = 0; i < equippedCharms.Length; i++)
        {
            if (equippedCharms[i].description == "")
            {
                return i;
            }
        }

        return -1; //if all slots are equiped
    }

    private void SetColorForSlot(int slotIndex)
    {
        // check if slot has charm in it
        if (slotIndex >= 0 && slotIndex < equippedCharms.Length)
        {
            switch (slotIndex)
            {
                case 0:
                    Image_one.sprite = equippedCharms[slotIndex].image.sprite;
                    break;
                case 1:
                    Image_two.sprite = equippedCharms[slotIndex].image.sprite;
                    break;
                case 2:
                    Image_three.sprite = equippedCharms[slotIndex].image.sprite;
                    break;
            }
        }
    }
    private void DeleteColorForSlot(int slotIndex)
    {
        // check if slot has charm in it
        if (slotIndex >= 0 && slotIndex < equippedCharms.Length)
        {
            switch (slotIndex)
            {
                case 0:
                    Image_one.sprite = basic;
                    break;
                case 1:
                    Image_two.sprite = basic;
                    break;
                case 2:
                    Image_three.sprite = basic;
                    break;
            }
        }
    }
    public void OnImageClick(int slotIndex)
    {
        if (equippedCharms[slotIndex].description != "")
        {
            UpdateDescription(slotIndex);
            Prompt.text = "";
            selectedCharmIndex = -1;
        }
        else
        {
            Prompt.text = "";
            nameText.text = "";
            descriptionText.text = "select skill";
        }

    }

    private void UpdateDescription(int charmIndex)
    {
        int descriptionIndex = charmIndex;

        if (descriptionIndex >= 0 && descriptionIndex < equippedCharms.Length)
        {
            descriptionText.text = equippedCharms[descriptionIndex].description;
            nameText.text = equippedCharms[descriptionIndex].name;
        }
        else
        {
            Debug.LogError("Invalid description index: " + descriptionIndex);
        }
    }


    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }


    private void Load()
    {
        var data = SaveManager.Load<SaveData.CharmsSave>(saveKey);

        for (int i = 0; i < charms.Length; i++)
        {
            charms[i].isOwned = data._isOwned[i];
        }

        if(data._isEqiped[0] != -1)
        {
            EquipCharm(data._isEqiped[0]);
        }
        if (data._isEqiped[1] != -1)
        {
            EquipCharm(data._isEqiped[1]);
        }
        if (data._isEqiped[2] != -1)
        {
            EquipCharm(data._isEqiped[2]);
        }

        SetColorForSlot(0);
        SetColorForSlot(1);
        SetColorForSlot(2);
        UpdateCharmImages();
    }

    private SaveData.CharmsSave GetData()
    {
        var data = new SaveData.CharmsSave()
        {
            _isEqiped = new int[3],
            _isOwned = new bool[charms.Length]
        };

       data._isEqiped[0] = GetEquipedCharmInList(0);
       data._isEqiped[1] =  GetEquipedCharmInList(1);
       data._isEqiped[2] = GetEquipedCharmInList(2);


        for (int i = 0; i < charms.Length; i++)
        {
            data._isOwned[i] = charms[i].isOwned;
        }

        return data;
    }

}