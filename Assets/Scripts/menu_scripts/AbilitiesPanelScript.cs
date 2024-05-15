using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Ability
{
    public Image image;
    public bool isOwned;
    public string description;
    public string name;
    public bool isEquiped;
}

public class AbilitiesPanelScript : MonoBehaviour
{
    public static AbilitiesPanelScript instance;

    [SerializeField] public Ability[] abilities;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text promptText;
    [SerializeField] private Text equipedText;
    private int selectedAbilityIndex = -1;
    private bool equiped = false;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        descriptionText.text = "select ability";
        nameText.text = "";
        promptText.text = "";
        UpdateAbilityImages();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (selectedAbilityIndex != -1)
            {
                abilities[selectedAbilityIndex].image.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            descriptionText.text = "select ability";
            nameText.text = "";
            promptText.text = "";
            UpdateAbilityImages();
        }
    }

    public void OnAbilityImageClick(int abilityIndex)
    {
        UpdateDescriptionText(abilityIndex);

        if (selectedAbilityIndex != -1)
        {
            abilities[selectedAbilityIndex].image.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (abilities[abilityIndex].isOwned)
        {
            abilities[abilityIndex].image.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            selectedAbilityIndex = abilityIndex;
            nameText.text = abilities[abilityIndex].name;
        }

        if (!abilities[abilityIndex].isEquiped && abilities[abilityIndex].isOwned)
        {
            promptText.text = "equip this ability?";
        }
        else if (abilities[abilityIndex].isEquiped && abilities[abilityIndex].isOwned) promptText.text = "unequip this ability?";
    }

    private void UpdateDescriptionText(int abilityIndex)
    {
        if (abilityIndex >= 0 && abilityIndex < abilities.Length && abilities[abilityIndex].isOwned)
        {
            Debug.Log(abilities[abilityIndex].description);
            descriptionText.text = abilities[abilityIndex].description;
        }
        else
        {
            //  Debug.LogError("Invalid description index: " + talantIndex);
        }
    }
    private void UpdateAbilityImages()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            // Set image color based on whether the charm is purchased or not
            abilities[i].image.color = abilities[i].isOwned ? Color.white : Color.black;
        }
    }

    public void EquipAbility()
    {
        int abilityIndex = selectedAbilityIndex;
        int equippedAbilityIndex = FindIndexOfEquippedAbility();
        if (equiped == false)
        {
            abilities[abilityIndex].isEquiped = true;
            equiped = true;
            UpdateDescriptionText(abilityIndex);
            nameText.text = abilities[abilityIndex].name;
            promptText.text = "equiped " + nameText.text;
            equipedText.text = "Equiped: " + nameText.text;
        }
        else {
            if (abilities[abilityIndex].isOwned && !abilities[abilityIndex].isEquiped)
            {
                abilities[abilityIndex].isEquiped = true;
                abilities[equippedAbilityIndex].isEquiped = false;
                UpdateDescriptionText(abilityIndex);
                nameText.text = abilities[abilityIndex].name;
                promptText.text = "replaced with " + nameText.text;
                equipedText.text = "Equiped: " + nameText.text;
            }
            else if (abilities[abilityIndex].isOwned && abilities[abilityIndex].isEquiped)
            {
                abilities[abilityIndex].isEquiped = false;
                equiped = false;
                promptText.text = "unequiped " + nameText.text;
                equipedText.text = "Equiped: " ;
            }
            else
            {
                descriptionText.text = "You do not own this ability yet";
            }
        }
    }

    private int FindIndexOfEquippedAbility()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i].isEquiped)
            {
                return i;
            }
        }
        return -1;
    }
}
