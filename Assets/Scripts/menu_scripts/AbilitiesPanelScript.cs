using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
    private BossGUI bossGUI;
    private Bosstwo bosstwo;
    [SerializeField] public Ability[] abilities;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text promptText;
    [SerializeField] private Text equipedText;
    private int selectedAbilityIndex = -1;
    private bool equiped = false;
    private int index;
    private int abilityIndex;
    private string saveKey = "PlayerAbilities";

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
        bossGUI = FindAnyObjectByType<BossGUI>();
        bosstwo = FindAnyObjectByType<Bosstwo>();
        descriptionText.text = "select ability";
        nameText.text = "";
        promptText.text = "";
        UpdateAbilityImages();
        Load();
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

    public void SetAbilityOwned(int index)
    {
        abilities[index].isOwned = true;
        Save();
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
        if (selectedAbilityIndex != -1)
        {
            abilityIndex = selectedAbilityIndex;
            index = abilityIndex;
        }
       
        int equippedAbilityIndex = FindIndexOfEquippedAbility();
        if (abilities[abilityIndex].isOwned && equiped == false)
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
                index = -1;
            }
            else
            {
                descriptionText.text = "You do not own this ability yet";
            }
        }
        Save();
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


    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }


    private void Load()
    {
        var data = SaveManager.Load<SaveData.AbilitySave>(saveKey);

        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i].isOwned = data._isOwned[i];
        }

        if (data._isEqiped != -1 && abilities[data._isEqiped].isOwned)
        {
            abilityIndex = data._isEqiped;
            EquipAbility();
        }
        
        UpdateAbilityImages();

    }

    private SaveData.AbilitySave GetData()
    {
        var data = new SaveData.AbilitySave()
        {
            _isEqiped = index,
            _isOwned = new bool[abilities.Length]
        };

        for (int i = 0; i < abilities.Length; i++)
        {
            data._isOwned[i] = abilities[i].isOwned;
        }

        return data;
    }
}
