using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct WeaponSkill
{
    public Image image;
    public int cost;
    public bool isPurchased;
    public string description;
    public int[] requiredSkill;
    public bool isBasicSkill;
}

public class CharacterPanelScript : MonoBehaviour
{
    PauseMenuScript pauseMenuScript;
    [SerializeField] private CharacterStats playerStats;

    [SerializeField] private Text[] statTextsFloat;
    [SerializeField] private WeaponStyle[] weaponStyles;
    [SerializeField] private WeaponSkill[] skills;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject confrim;
    [SerializeField] private Text confrimText;
    [SerializeField] private Text Prompt;
    [SerializeField] private Image style;
    private SphereGUI sphereGUI;
    private int lastPicked;
    private int currentStyle;
    private int selectedSkillIndex = -1;
    bool allRequiredSkillsPurchased;
    private string saveKey = "PlayerWeaponSkills";
    private string saveKey2 = "achivementsSave";
    private SaveData.AchivementsSave data2;

    private int isEqiped;
    private void Awake()
    {
        weaponStyles = FindObjectsOfType<WeaponStyle>();
        sphereGUI = FindAnyObjectByType<SphereGUI>();
        data2 = SaveManager.Load<SaveData.AchivementsSave>(saveKey2);

    }
    private void Start()
    {
        pauseMenuScript = GetComponent<PauseMenuScript>();

        if (pauseMenuScript == null)
        {
            Debug.LogError("PauseMenuScript not found!");
            return;
        }
        Load();
        currentStyle = 1;
        descriptionText.text = "select weapon";
        Prompt.text = "";
        UpdateSkillImages();
        SetStats(playerStats.currentHealth, playerStats.GetMaxHealthValue());

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetStats(playerStats.currentHealth, playerStats.GetMaxHealthValue());
            descriptionText.text = "select weapon";
            Prompt.text = "";
        }
    }

    private void UpdateSkillImages()
    {
        foreach (var skill in skills)
        {
            if (skill.image != null)
            {
                if (skill.isPurchased)
                {
                    skill.image.color = Color.white;
                }
                else
                {
                    skill.image.color = Color.gray;
                }
            }
        }
    }

    private void EquipSkill(int skillIndex)
    {
        if(skillIndex >= 0)
        {
            WeaponSkill selectedSkill = skills[skillIndex];
            if (selectedSkill.isBasicSkill)
            {
                int pickedStyle = skillIndex / 3;
                if (pickedStyle != lastPicked)
                    weaponStyles[lastPicked].DeactivateEffect();
                lastPicked = pickedStyle;
                bool isUpgradedOnce = false;
                bool isUpgradedTwice = false;

                foreach (var skill in skills)
                {
                    if (skill.isPurchased && skill.requiredSkill.Length > 0)
                    {
                        // Check if the skill requiring the current skill is upgraded
                        if (skills[skillIndex + 1].isPurchased)
                        {
                            isUpgradedOnce = true;

                            /// Check if the second-level skill requiring the current skill is upgraded
                            if (skills[skillIndex + 2].isPurchased)
                            {
                                isUpgradedOnce = false;
                                isUpgradedTwice = true;
                            }
                        }
                    }
                }

                if (isUpgradedTwice)
                {
                    SetStyle(skillIndex);
                    Prompt.text = "skill equiped";
                    Debug.Log("Equipped Skill " + skillIndex + " with two upgrades");
                    weaponStyles[pickedStyle].ActivateFirstUpgrade();
                    weaponStyles[pickedStyle].ActivateSecondUpgrade();
                    weaponStyles[pickedStyle].ActivateThirdUpgrade();

                }
                else if (isUpgradedOnce)
                {
                    SetStyle(skillIndex);
                    Prompt.text = "skill equiped";
                    Debug.Log("Equipped Skill " + skillIndex + " with one upgrade");
                    weaponStyles[pickedStyle].ActivateFirstUpgrade();
                    weaponStyles[pickedStyle].ActivateSecondUpgrade();
                }
                else
                {
                    SetStyle(skillIndex);
                    Prompt.text = "skill equiped";
                    Debug.Log("Equipped Skill " + skillIndex + " without upgrades");
                    weaponStyles[pickedStyle].ActivateFirstUpgrade();

                }
            }
            else
            {
                int baseSkillIndex = GetBaseSkillIndex(skillIndex);

                Debug.Log("set style color" + baseSkillIndex);
                Debug.Log("Skill Index: " + skillIndex + " is not a basic skill and cannot be equipped directly.");
                isEqiped = baseSkillIndex;
                EquipSkill(baseSkillIndex);
            }
        }
        Save();
    }
    private bool GetRequiedSkills(WeaponSkill currentSkill)
    {
        foreach (int requiredSkillIndex in currentSkill.requiredSkill)
        {
            if (requiredSkillIndex >= 0 && requiredSkillIndex < skills.Length)
            {
                if (!skills[requiredSkillIndex].isPurchased)
                {
                    Debug.Log("no");
                    return false;
                }
              
            }
            else
            {
                return false;
            }
        }
        Debug.Log("yes");
        return true;
    }
    public void PurchaseSkill()
    {
        int skillIndex = selectedSkillIndex;
        if (skillIndex < skills.Length && skillIndex != -1)
        {
            WeaponSkill currentSkill = skills[skillIndex];

            allRequiredSkillsPurchased = GetRequiedSkills(currentSkill);
            if (allRequiredSkillsPurchased)
            {
                if (!currentSkill.isPurchased)
                {
                    int skillCost = currentSkill.cost;

                    if (pauseMenuScript.GetEssenceAmount(0) >= skillCost)
                    {
                        pauseMenuScript.UseEssenceAmount(skillCost);
                        Prompt.text = "skill purshcased";
                        SetSkillPurchased(skillIndex, true);
                        EquipSkill(skillIndex);
                        isEqiped = skillIndex;
                        Debug.Log("Purchased Skill Index: " + skillIndex);
                        confrimText.text = "Confrim";
                        if (!skills[skillIndex].isBasicSkill)
                        {
                            confrim.SetActive(false);
                        }
                    }
                    else
                    {
                        descriptionText.text = "Unable to buy, not enough money";
                    }
                }
                else
                {
                    Debug.Log("Skill Index: " + skillIndex + " is already purchased.");
                    EquipSkill(skillIndex);
                    isEqiped = skillIndex;
                    SetStyle(skillIndex);
                }
            }
            else
            {
                descriptionText.text = "Unable to buy, required skill is not purchased";
            }
            UpdateSkillImages();
            Save();
        }

        if (!data2._isOwned[7])
        {
            if (skills[2].isPurchased || skills[5].isPurchased || skills[8].isPurchased)
            {
                MainMenu.instance.setAchivementOwned(7);
            }
        }
        if (!data2._isOwned[9])
        {
            if (skills[2].isPurchased && skills[5].isPurchased && skills[8].isPurchased)
            {
                MainMenu.instance.setAchivementOwned(9);
            }
        }
    }

    private int GetBaseSkillIndex(int upgradeIndex)
    {
        switch (upgradeIndex)
        {
            case 1:
            case 2:
                return 0;
            case 4:
            case 5:
                return 3;
            case 7:
            case 8:
                return 6;
            default:
                return -1;
        }
    }

    private void SetStyle(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0:
                style.color = Color.red;
                Debug.Log("CHANGED");
                break;
            case 3:
                style.color = Color.green;
                break;
            case 6:
                style.color = Color.blue;
                break;
            default:
                style.color = Color.white;
                break;
        }
    }

    private void SetSkillPurchased(int skillIndex, bool value)
    {
        WeaponSkill[] updatedSkills = skills;
        updatedSkills[skillIndex].isPurchased = value;
        skills = updatedSkills;
    }
    public void OnSkillImageClick(int skillIndex)
    {
        WeaponSkill currentSkill = skills[skillIndex];
        allRequiredSkillsPurchased =  GetRequiedSkills(currentSkill);

        int clickedStyle = skillIndex / 3 + 1;
        if (clickedStyle != currentStyle)
        {
            currentStyle = clickedStyle;
            UpdateDescriptionText(0);
        }

        int descriptionIndex = skillIndex % 3;
        UpdateDescriptionText(descriptionIndex);

        if (selectedSkillIndex != -1)
        {
            UpdateSkillImageScale(selectedSkillIndex, 1f);
        }

        UpdateSkillImageScale(skillIndex, 1.1f);
        selectedSkillIndex = skillIndex;

        if (allRequiredSkillsPurchased) {
            confrim.SetActive(true);
            if (!skills[skillIndex].isPurchased)
            {
                confrimText.text = "Upgrade";
                Prompt.text = "Do you wish to buy this skill?";
                confrim.SetActive(true);

            }
            else if (skills[skillIndex].isPurchased && skills[skillIndex].isBasicSkill)
            {
                confrimText.text = "Confrim";
                Prompt.text = "Do you wish to equip this skill?";
                confrim.SetActive(true);
            }
            else if (skills[skillIndex].isPurchased && !skills[skillIndex].isBasicSkill)
            {
                confrim.SetActive(false);
                Prompt.text = "";
            }
        }
        else
        {
            confrim.SetActive(false);
            Prompt.text = "";
        }
   
    }

    private void UpdateSkillImageScale(int skillIndex, float scale)
    {
        WeaponSkill currentSkill = skills[skillIndex];
        for (int i = 0; i < skills.Length; i++)
        {
            // Reduce the scale of all skill images except the clicked one
            if (i != skillIndex)
            {
                skills[i].image.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                if (!currentSkill.isPurchased || currentSkill.isBasicSkill)
                {
                    skills[skillIndex].image.transform.localScale = new Vector3(scale, scale, scale);
                }
                else
                {
                    skills[skillIndex].image.transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }
    }

    private void UpdateDescriptionText(int skillIndex)
    {
        int descriptionIndex = skillIndex;

        string[] descriptions = null;

        if (currentStyle >= 1 && currentStyle <= 3)
        {
            int styleIndex = currentStyle - 1;
            descriptions = new string[] { skills[styleIndex * 3].description, skills[styleIndex * 3 + 1].description, skills[styleIndex * 3 + 2].description };
        }

        if (descriptions != null && descriptionIndex >= 0 && descriptionIndex < descriptions.Length)
        {
            descriptionText.text = descriptions[descriptionIndex];
        }
        else
        {
            Debug.LogError("Invalid description index: " + descriptionIndex);
        }
    }

    private void SetStats(float _currentHealth, float _maxHealth/*/, int _currentMana, 
        int _maxMana, int _collectedCollectibles, int _maxCollectibles, int _completionPercentage/*/)
    {

        SetStatTextFloat(0, _currentHealth, _maxHealth); // Health
        //SetStatTextFloat(1, currentMana, maxMana); // Mana
        //SetStatTextInt(2, collectedCollectibles, maxCollectibles); // Completion Percentage
        //SetStatTextInt(3, completionPercentage, 100); // Completion Percentage
    }

    private void SetStatTextFloat(int index, float currentValue, float maxValue)
    {
        if (index >= 0 && index < statTextsFloat.Length)
        {
            statTextsFloat[index].text = string.Format("{0}/{1}", currentValue, maxValue);
        }
    }

    private void SetStatTextInt(int index, int value, int maxValue)
    {
        if (index >= 0 && index < statTextsFloat.Length)
        {

            if (index == 4)
            {
                statTextsFloat[index].text = $"{value}%";
            }
            else
            {
                statTextsFloat[index].text = $"{value}/{maxValue}";
            }
        }

    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());

    }


    private void Load()
    {
        var data = SaveManager.Load<SaveData.WeaponSave>(saveKey);

        
        isEqiped = data._isEqiped;
        
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].isPurchased = data._isOwned[i];
        }
       
        UpdateSkillImages();
        if(isEqiped!= -1)
        {
            EquipSkill(isEqiped);
            SetStyle(isEqiped);
        }
    }

    private SaveData.WeaponSave GetData()
    {

        var data = new SaveData.WeaponSave()
        {
            _isEqiped = isEqiped,
            _isOwned = new bool[skills.Length]
        };

        for (int i = 0; i < skills.Length; i++)
        {
            data._isOwned[i] = skills[i].isPurchased;
        }

        return data;
    }
}
