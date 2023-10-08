using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject gameMenu; 
    public GameObject optionsMenu; 
    public GameObject achievementsMenu;

    public GameObject resolutionSettings; 
    public GameObject qualitySettings;   
    public GameObject soundSettings;
    private GameObject currentActiveSettings;

    public TMP_Dropdown resolutionDropdown; 
    public string[] resolutions = new string[] { "1920x1080", "1280x720", "1600x900", "2560x1440" };
    private bool isFullScreen = true;

    private int currentQualityLevel = 2;
    public Toggle lowQualityToggle;
    public Toggle mediumQualityToggle;
    public Toggle highQualityToggle;

    public Slider volumeSlider;

    void Start()
    {
        Screen.fullScreen = isFullScreen;
        volumeSlider.value = 0.5f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
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
    void OnResolutionChanged(int resolutionIndex)
    {
        string selectedResolution = resolutions[resolutionIndex];
        string[] resolutionParts = selectedResolution.Split('x');

        int width = int.Parse(resolutionParts[0]);
        int height = int.Parse(resolutionParts[1]);

        Screen.SetResolution(width, height, Screen.fullScreen);
        Debug.Log("Changed resolution to: " + width + "x" + height);
    }
    public void ToggleFullScreen()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
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
        }
        else if (!lowQualityToggle.isOn && !mediumQualityToggle.isOn && !highQualityToggle.isOn)
        {
            // Если все Toggle выключены, включаем средний уровень (medium)
            mediumQualityToggle.isOn = true;
            currentQualityLevel = 2;
            QualitySettings.SetQualityLevel(currentQualityLevel);
        }
    }
    public void ActivateSoundSettings()
    {
        ActivateSettings(soundSettings);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
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
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
