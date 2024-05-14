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

    private void Awake()
    {
        charmsArray = FindObjectsOfType<Charm>();
    }
    void Start()
    {
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


    public void OnEquipedCharmImageClick(int charmIndex) { 
        confirm.SetActive(false);
        Prompt.text = "";
        UpdateDescriptionText(charmIndex);
    }

    public void OnCharmImageClick(int charmIndex)
    {
        confirm.SetActive(true);
        UpdateDescriptionText(charmIndex);
        nameText.text = charms[charmIndex].name;

        if (selectedCharmIndex != -1)
        {
            charms[selectedCharmIndex].image.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        charms[charmIndex].image.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        selectedCharmIndex = charmIndex;

        if (!charms[charmIndex].isEquiped)
        {
            Prompt.text = "equip this charm?";
        }
        else Prompt.text = "unequip this charm?";

    }

    private void UpdateDescriptionText(int charmIndex)
    {
        if (charmIndex >= 0 && charmIndex < charms.Length)
        {
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

            // Check if the charm is available
            if (currentCharm.isOwned && !currentCharm.isEquiped)
            {
                Prompt.text = "Charm equiped";
                // If the charm is already owned, it can be equipped
                EquipCharm(charmIndex);
            }
            else if (!currentCharm.isOwned)
            {
                if (currentCharm.cost == 0)
                {
                    Prompt.text = "Charm unowned";
                    descriptionText.text = "This charm is not available for purchase.";
                }
                else if (pauseMenuScript.GetEssenceAmount(0) >= currentCharm.cost)
                {
                    pauseMenuScript.UseEssenceAmount(currentCharm.cost);
                    Prompt.text = "charm purshcased";
                    charms[charmIndex].isOwned = true;
                    Debug.Log(currentCharm.isOwned);
                    UpdateCharmImages();
                    
                }
                else {
                    Prompt.text = "Charm unowned";
                    descriptionText.text = "Not enough money.";
                }
               
            }
            else if (currentCharm.isOwned && currentCharm.isEquiped)
            {
                Prompt.text = "Charm unequiped";
                UnequipeCharm(charmIndex);
            }
        }
        else
        {
            descriptionText.text = "You have not selected a charm.";
        }

        UpdateCharmImages();
    }

    public void LastClicked(int index)
    {
        lastClickedIndex = index;
    }

    private void SetCharmOwned(int charmIndex, bool value)
    {
        CharmInfo[] updatedCharms = charms;

        if (charmIndex < updatedCharms.Length)
        {
            updatedCharms[charmIndex].isOwned = value;
            charms = updatedCharms;

            // Add logic to handle the charm discovery
            if (value)
            {
                Debug.Log("You found Charm Index: " + charmIndex);
            }
        }
    }
    private void UnequipeCharm(int charmIndex)
    {
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
        // Check if the charm is unique and already equipped
        if (charms[charmIndex].isUnique)
        {
            int equippedUniqueIndex = FindIndexOfEquippedUniqueCharm();
            int previousIndex = FindIndexOfEquippedUniqueCharmInLIst();
            Debug.Log("check for unique " + equippedUniqueIndex);
            if (equippedUniqueIndex != -1)
            {
                Debug.Log("found unique ");       
                // If a unique charm is already equipped and it's different, replace it with the new one
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

        // Check if there are free slots for equipping
        int freeSlot = FindFreeSlot();

        if (freeSlot != -1)
        {
            // Fill the free slot with the charm's data
            equippedCharms[freeSlot] = charms[charmIndex];

            charms[charmIndex].isEquiped = true;
            SetColorForSlot(freeSlot);
            UpdateDescription(freeSlot);
            
        }
        else
        {
            // If all slots are occupied and a specific slot is chosen, insert into that slot
            if (lastClickedIndex != -1)
            {
                charms[lastClickedIndex].isEquiped = false;
                equippedCharms[lastClickedIndex] = charms[charmIndex];

                charms[charmIndex].isEquiped = true;
                SetColorForSlot(lastClickedIndex);
                UpdateDescription(lastClickedIndex);
               
                //lastClickedIndex = -1;
            }
            else
            {
                // If all slots are occupied and no slot is chosen, insert into the first slot
                charms[0].isEquiped = false;
                equippedCharms[0] = charms[charmIndex];

                charms[charmIndex].isEquiped = true;
                SetColorForSlot(0);
                UpdateDescription(0);
               
            }
        }
        if (charms[charmIndex].isEquiped)
        {
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
                for (int j = 0; j < charms.Length; j++) {
                    if (equippedCharms[i].description == charms[j].description) {
                        return j;
                    }
                }
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
        else {
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

}


