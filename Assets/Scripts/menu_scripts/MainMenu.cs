using SaveData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct Achivement
{
    public Image image;
    public bool isOwned;
    public string description;
    public string name;
}

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject achievementsMenu;

    [SerializeField] private GameObject resolutionSettings;
    [SerializeField] private GameObject qualitySettings;
    [SerializeField] private GameObject soundSettings;
    [SerializeField] private GameObject currentActiveSettings;

    [SerializeField] private Slider volumeSlider;

  

    [SerializeField] private Toggle lowQualityToggle;
    [SerializeField] private Toggle mediumQualityToggle;
    [SerializeField] private Toggle highQualityToggle;

    [SerializeField] private Text descriptionText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text fullscreenText;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private string[] resolutions = new string[] { "1920x1080", "1280x720", "1600x900", "2560x1440" };
    private bool isFullScreen;
    private int currentQualityLevel;
    private int width;
    private int height;
    private string currentResolution;

    public Achivement[] achivements = new Achivement[14];
    private int selectedAchivementIndex = -1;
    private string saveKey = "mainMenuSettings";
    private string saveKey2 = "playerPosition";
    private int sceneIndex;

    void Start()
    {
        Load();
        //currentResolution = $"{width}x{height}";

        descriptionText.text = "";
        nameText.text = "";
        UpdateAchivementImages();
    }



    public void ShowOptions()
    {
        gameMenu.SetActive(false);
        optionsMenu.SetActive(true);
        resolutionSettings.SetActive(false);
        qualitySettings.SetActive(false);
        soundSettings.SetActive(false);

        currentActiveSettings = null;

    }
    public void ActivateResolutionSettings()
    {
        ActivateSettings(resolutionSettings);
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.ToList());

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);


    }
    public void OnResolutionChanged(int resolutionIndex)
    {
        string selectedResolution = resolutions[resolutionIndex];
        string[] resolutionParts = selectedResolution.Split('x');

        width = int.Parse(resolutionParts[0]);
        height = int.Parse(resolutionParts[1]);

        Screen.SetResolution(width, height, Screen.fullScreen);
        Debug.Log("Changed resolution to: " + width + "x" + height);
        Save();

    }
    public void ToggleFullScreen()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            fullscreenText.text = "Fullscreen";
        }
        else
            fullscreenText.text = "Window";
        Save();

    }

    public void ActivateQualitySettings()
    {
        ActivateSettings(qualitySettings);

        lowQualityToggle.onValueChanged.AddListener(newValue => OnQualityToggleValueChanged(newValue, 0));
        mediumQualityToggle.onValueChanged.AddListener(newValue => OnQualityToggleValueChanged(newValue, 2));
        highQualityToggle.onValueChanged.AddListener(newValue => OnQualityToggleValueChanged(newValue, 5));

    }

    void UpdateToggles()
    {
        lowQualityToggle.isOn = currentQualityLevel == 0;
        mediumQualityToggle.isOn = currentQualityLevel == 2;
        highQualityToggle.isOn = currentQualityLevel == 5;

    }

    void OnQualityToggleValueChanged(bool newValue, int qualityLevel)
    {
        if (newValue)
        {
            currentQualityLevel = qualityLevel;
            QualitySettings.SetQualityLevel(currentQualityLevel);
            UpdateToggles();
            Save();
        }
        else if (!lowQualityToggle.isOn && !mediumQualityToggle.isOn && !highQualityToggle.isOn)
        {
            mediumQualityToggle.isOn = true;
            currentQualityLevel = 2;
            QualitySettings.SetQualityLevel(currentQualityLevel);
            Save();
        }

    }
    public void ActivateSoundSettings()
    {
        ActivateSettings(soundSettings);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        Save();
    }
    void ActivateSettings(GameObject settings)
    {
        if (currentActiveSettings != null)
        {
            currentActiveSettings.SetActive(false);
        }

        settings.SetActive(true);
        currentActiveSettings = settings;
    }

    public void ShowAchievements()
    {
        gameMenu.SetActive(false);
        achievementsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        optionsMenu.SetActive(false);
        achievementsMenu.SetActive(false);
        gameMenu.SetActive(true);

        descriptionText.text = "";
        nameText.text = "";
    }
    void UpdateDescriptionText(int achivementIndex)
    {
        if (achivementIndex >= 0 && achivementIndex < achivements.Length && achivements[achivementIndex].isOwned)
        {
            Debug.Log(achivements[achivementIndex].description);
            descriptionText.text = achivements[achivementIndex].description;
        }
    }
    public void OnAchivementImageClick(int achivementIndex)
    {
        UpdateDescriptionText(achivementIndex);

        if (selectedAchivementIndex != -1)
        {
            achivements[selectedAchivementIndex].image.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (achivements[achivementIndex].isOwned)
        {
            achivements[achivementIndex].image.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            selectedAchivementIndex = achivementIndex;
            nameText.text = achivements[achivementIndex].name;
        }
    }
    void UpdateAchivementImages()
    {
        for (int i = 0; i < achivements.Length; i++)
        {
            // Set image color based on whether the charm is purchased or not
            achivements[i].image.color = achivements[i].isOwned ? Color.white : Color.gray;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Save()
    {
        SaveManager.Save(saveKey, GetData());

    }
    private void SetFull(bool status)
    {
        isFullScreen = status;
    }
    public void StartGame()
    {
        PlayerPrefs.DeleteKey("playerPosition");
        PlayerPrefs.DeleteKey("playerMoneySave");
        PlayerPrefs.DeleteKey("playerSave");
        PlayerPrefs.DeleteKey("flasks");
        PlayerPrefs.DeleteKey("PlayerWeaponSkills");
        PlayerPrefs.DeleteKey("PlayerAbilities");
        PlayerPrefs.DeleteKey("PlayerCharms");
        PlayerPrefs.DeleteKey("PlayerTalants");
        PlayerPrefs.DeleteKey("PlayerItems");
        PlayerPrefs.DeleteKey("LostMoney");
        PlayerPrefs.DeleteKey("LostStatusMoney");
        PlayerPrefs.DeleteKey("StatueSave");
        PlayerPrefs.DeleteKey("camera");
        PlayerPrefs.DeleteKey("BossSave");
        PlayerPrefs.DeleteKey("doorSave");
        PlayerPrefs.DeleteKey("mapSave");
        PlayerPrefs.DeleteKey("IconsSave");
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        var data2 = SaveManager.Load<SaveData.PlayerPos>(saveKey2);
        sceneIndex = data2._sceneIndex;
        Debug.Log(sceneIndex);
        Debug.Log(data2._playerPos);
       SceneManager.LoadScene(sceneIndex);

    }
    private void Load()
    {
        var data = SaveManager.Load<SaveData.MainSettings>(saveKey);
        SetFull(data._isFullScreen);
        Screen.fullScreen = isFullScreen;
        fullscreenText.text = isFullScreen ? "Fullscreen" : "Window";

        currentQualityLevel = data._currentQualityLevel;
        UpdateToggles();

        // Перевірка на значення 0 і встановлення за замовчуванням
        if (data._width == 0 || data._height == 0)
        {
            string defaultResolution = resolutions[0];
            string[] resolutionParts = defaultResolution.Split('x');
            width = int.Parse(resolutionParts[0]);
            height = int.Parse(resolutionParts[1]);
        }
        else
        {
            width = data._width;
            height = data._height;
        }

        Screen.SetResolution(width, height, Screen.fullScreen);
        volumeSlider.value = data._volumeValue;

        string savedResolution = $"{width}x{height}";
        Debug.Log($"{savedResolution}");

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.ToList());

        int savedResolutionIndex = Array.IndexOf(resolutions, savedResolution);
        if (savedResolutionIndex != -1)
        {
            resolutions = resolutions.OrderBy(r => r != savedResolution).ToArray();
        }
        else
        {
            resolutionDropdown.value = 0; // Встановлюємо перше значення за замовчуванням
        }

        Debug.Log(isFullScreen);
        Debug.Log(data._isFullScreen);
    }

    private SaveData.MainSettings GetData()
    {
        var data = new SaveData.MainSettings()
        {
            _isFullScreen = isFullScreen,
            _currentQualityLevel = currentQualityLevel,
            _height = height,
            _width = width,
            _volumeValue = volumeSlider.value,
        };
        return data;
    }
}
