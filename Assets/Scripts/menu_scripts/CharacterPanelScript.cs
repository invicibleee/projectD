using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public Text[] statTextsFloat;
    public WeaponStyle[] weaponStyles;
    public WeaponSkill[] skills;
    public Text descriptionText;
    public GameObject confrim;
    public Text confrimText;
    public Text Prompt;
    public Image style;

    private int currentStyle;
    private int selectedSkillIndex = -1;

    private void Awake()
    {
        weaponStyles = FindObjectsOfType<WeaponStyle>();
    }
    private void Start()
    {
        pauseMenuScript = GetComponent<PauseMenuScript>();

        if (pauseMenuScript == null)
        {
            Debug.LogError("PauseMenuScript not found!");
            return;
        }

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

    void UpdateSkillImages()
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


    void EquipSkill(int skillIndex)
    {
        WeaponSkill selectedSkill = skills[skillIndex];
   
        if (selectedSkill.isBasicSkill)
        {
            // If the selected skill is basic, it can be equipped
            Debug.Log("Equipped Skill Index: " + skillIndex);

           
            bool isUpgradedOnce = false;
            bool isUpgradedTwice = false;

            foreach (var skill in skills)
            {
                if (skill.isPurchased && skill.requiredSkill.Length > 0)
                {
                    // Check if the skill requiring the current skill is upgraded
                    if ( skills[skillIndex + 1].isPurchased)
                    {
                        isUpgradedOnce = true;
                        Debug.Log(isUpgradedOnce);

                        /// Check if the second-level skill requiring the current skill is upgraded
                        if (skills[skillIndex + 2].isPurchased)
                        {
                            isUpgradedOnce = false;
                            isUpgradedTwice = true;
                        }
                    }
                }
            }

            //// Check for upgrades and display the corresponding message
            //if (isUpgradedTwice)
            //{
            //    Prompt.text = "skill equiped";
            //    Debug.Log("Equipped Skill " + skillIndex + " with two upgrades");
            //    weaponStyles[currentStyle].ActivateFirstUpgrade();
            //    weaponStyles[currentStyle].ActivateSecondUpgrade();
            //}
            //else if (isUpgradedOnce)
            if (isUpgradedOnce)
            {
                Prompt.text = "skill equiped";
                Debug.Log("Equipped Skill " + skillIndex + " with one upgrade");
                weaponStyles[2].ActivateFirstUpgrade();
            }
            //else
            //{
            //    Prompt.text = "skill equiped";
            //    Debug.Log("Equipped Skill " + skillIndex + " without upgrades");
            //    weaponStyles[currentStyle].ActivateFirstUpgrade();
            //    weaponStyles[currentStyle].ActivateSecondUpgrade();
            //    weaponStyles[currentStyle].ActivateThirdUpgrade();
            //}
        }
        else
        {
           
            Debug.Log("Skill Index: " + skillIndex + " is not a basic skill and cannot be equipped directly.");
        }
    }


    public void PurchaseSkill()
    {
        int skillIndex = selectedSkillIndex;
        if (skillIndex < skills.Length)
        {
            WeaponSkill currentSkill = skills[skillIndex];

            bool allRequiredSkillsPurchased = true;
            foreach (int requiredSkillIndex in currentSkill.requiredSkill)
            {
                if (requiredSkillIndex >= 0 && requiredSkillIndex < skills.Length)
                {
                    if (!skills[requiredSkillIndex].isPurchased)
                    {
                        allRequiredSkillsPurchased = false;
                        break;
                    }
                }
                else
                {
                    allRequiredSkillsPurchased = false;
                    break;
                }
            }

            if (allRequiredSkillsPurchased)
            {
                if (!currentSkill.isPurchased)
                {
                    int skillCost = currentSkill.cost;

                    if (pauseMenuScript.GetCurrency(0) >= skillCost)
                    {
                        pauseMenuScript.UseCurrency(skillCost);
                        Prompt.text = "skill purshcased";
                        SetSkillPurchased(skillIndex, true);
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
                    SetStyle(skillIndex);
                }
            }
            else
            {
                descriptionText.text = "Unable to buy, required skill is not purchased";
            }
            UpdateSkillImages();
        }
    }

    public void SetStyle(int skillIndex) {

        switch (skillIndex) {
            case 0:
                style.color = Color.red;
                break;
            case 3:
                style.color = Color.blue;
                break;
            case 6:
                style.color = Color.green;
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
        int clickedStyle = skillIndex / 3 + 1;
       // Debug.Log(skillIndex);
        if (clickedStyle != currentStyle)
        {
            currentStyle = clickedStyle;
            UpdateDescriptionText(0);
        }

        int descriptionIndex = skillIndex % 3;
        UpdateDescriptionText(descriptionIndex);

        if (selectedSkillIndex != -1)
        {
            // Reduce the scale of the previously selected skill image
            UpdateSkillImageScale(selectedSkillIndex, 1f);
        }

        // Increase the scale of the clicked skill image
        UpdateSkillImageScale(skillIndex, 1.1f);

        selectedSkillIndex = skillIndex;
        // Debug.Log("Selected Skill Index: " + selectedSkillIndex);

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
        else if (skills[skillIndex].isPurchased && !skills[skillIndex].isBasicSkill) {
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

    void UpdateDescriptionText(int skillIndex)
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
            //Debug.Log("Description: " + descriptions[descriptionIndex]);
        }
        else
        {
            Debug.LogError("Invalid description index: " + descriptionIndex);
            //descriptionText.text = "No description available";
        }
    }

    // Method to set health, stamina, and mana values
    public void SetStats(float _currentHealth, float _maxHealth/*/, int _currentMana, 
        int _maxMana, int _collectedCollectibles, int _maxCollectibles, int _completionPercentage/*/)
    {

        SetStatTextFloat(0, _currentHealth, _maxHealth); // Health
        //SetStatTextFloat(1, currentMana, maxMana); // Mana
        //SetStatTextInt(2, collectedCollectibles, maxCollectibles); // Completion Percentage
        //SetStatTextInt(3, completionPercentage, 100); // Completion Percentage
    }

    // Method to set the text of a specific float statistic
    void SetStatTextFloat(int index, float currentValue, float maxValue)
    {
        if (index >= 0 && index < statTextsFloat.Length)
        {
            statTextsFloat[index].text = string.Format("{0}/{1}", currentValue, maxValue);
        }
    }


    // Method to set the text of a specific integer statistic
    void SetStatTextInt(int index, int value, int maxValue)
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
}