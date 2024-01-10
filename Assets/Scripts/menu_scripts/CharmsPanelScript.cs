using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Charm
{
    public Image image;
    public int cost;
    public bool isOwned;
    public bool isUnique;
    public bool isEquiped;
    public string description;
}

public class CharmsPanelScript : MonoBehaviour
{

    public Charm[] charms;
    public Charm[] equippedCharms = new Charm[3];
    public Text descriptionText;
    public Text Prompt;

    private int currentCharm;
    private int selectedCharmIndex = -1;

    public Image Image_one;
    public Image Image_two;
    public Image Image_three;
    private int lastClickedIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        currentCharm = 1;
        descriptionText.text = "select skill";
        Prompt.text = "";
        UpdateCharmImages();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            descriptionText.text = "select skill";
            Prompt.text = "";
        }
    }


    public void OnCharmImageClick(int charmIndex)
    {
        int clickedCharm = charmIndex;

        if (clickedCharm != currentCharm)
        {
            currentCharm = clickedCharm;
            UpdateDescriptionText(0);
        }

        int descriptionIndex = charmIndex;
        UpdateDescriptionText(descriptionIndex);

        if (selectedCharmIndex != -1)
        {
            charms[selectedCharmIndex].image.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        charms[charmIndex].image.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        selectedCharmIndex = charmIndex;

        Prompt.text = "equip this charm?";
    }

    void UpdateDescriptionText(int charmIndex)
    {
        int descriptionIndex = charmIndex;

        if (descriptionIndex >= 0 && descriptionIndex < charms.Length)
        {
            descriptionText.text = charms[descriptionIndex].description;
        }
        else
        {
            Debug.LogError("Invalid description index: " + descriptionIndex);
        }
    }

    void UpdateCharmImages()
    {
        for (int i = 0; i < charms.Length; i++)
        {
            // Set image color based on whether the charm is purchased or not
            charms[i].image.color = charms[i].isOwned ? Color.white : Color.gray;
        }
    }

    public void PurchaseCharm()
    {
        int charmIndex = selectedCharmIndex;
        if (charmIndex >= 0 && charmIndex < charms.Length)
        {
            Charm currentCharm = charms[charmIndex];

            // Check if the charm is available
            if (currentCharm.isOwned)
            {
                Prompt.text = "Charm equiped";
                // If the charm is already owned, it can be equipped
                EquipCharm(charmIndex);
            }
            else
            {
                Prompt.text = "Charm unowned";
                descriptionText.text = "This charm is not available for purchase.";
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

    private void SetCharmPurchased(int charmIndex, bool value)
    {
        Charm[] updatedCharms = charms;
        updatedCharms[charmIndex].isOwned = value;
        charms = updatedCharms;
    }

    private void SetCharmOwned(int charmIndex, bool value)
    {
        Charm[] updatedCharms = charms;

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

    public void EquipCharm(int charmIndex)
    {
        // Check if there are free slots for equipping
        int freeSlot = FindFreeSlot();

        // Check if the charm is already equipped, exit the method
        if (IsCharmEquipped(charmIndex))
        {
            Prompt.text = "Charm already equiped";
            Debug.Log("Charm already equiped.");
            return;
        }

        if (freeSlot != -1)
        {
            // Fill the free slot with the charm's data
            equippedCharms[freeSlot] = charms[charmIndex];

            // Display the charm's image in the corresponding Image
            SetColorForSlot(freeSlot);

            // Display the charm's description
            UpdateDescription(freeSlot);
        }
        else
        {
            // If all slots are occupied and a specific slot is chosen, insert into that slot
            if (lastClickedIndex != -1)
            {
                equippedCharms[lastClickedIndex] = charms[charmIndex];

                // Display the charm's image in the corresponding Image
                SetColorForSlot(lastClickedIndex);

                // Display the charm's description
                UpdateDescription(lastClickedIndex);

                // Reset lastClickedIndex
                lastClickedIndex = -1;
            }
            else
            {
                // If all slots are occupied and no slot is chosen, insert into the first slot
                equippedCharms[0] = charms[charmIndex];

                // Display the charm's image in the corresponding Image
                SetColorForSlot(0);

                // Display the charm's description
                UpdateDescription(0);
            }
        }
    }


    private bool IsCharmEquipped(int charmIndex)
    {
        foreach (var equippedCharm in equippedCharms)
        {
            if (equippedCharm.Equals(charms[charmIndex]))
            {
                return true;
            }
        }
        return false;
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

    public void OnImageClick(int slotIndex)
    {
            UpdateDescription(slotIndex);
    }

    void UpdateDescription(int charmIndex)
    {
        int descriptionIndex = charmIndex;

        if (descriptionIndex >= 0 && descriptionIndex < equippedCharms.Length)
        {
            descriptionText.text = equippedCharms[descriptionIndex].description;
        }
        else
        {
            Debug.LogError("Invalid description index: " + descriptionIndex);
        }
    }

}


