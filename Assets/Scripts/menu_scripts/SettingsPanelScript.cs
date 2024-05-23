using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanelScript : MonoBehaviour
{
    public string[] resolutions = new string[] { "1920x1080", "1280x720", "1600x900", "2560x1440" };

    private bool isFullScreen ;
    private int currentQualityLevel ;
    private int width;
    private int height;
    public Toggle lowQualityToggle;
    public Toggle mediumQualityToggle;
    public Toggle highQualityToggle;

    public GameObject controls_settings;
    public GameObject resolution_settings; 
    public GameObject quality_settings; 
    public GameObject sound_settings;
    public GameObject video1;
    public GameObject video2;
    public Slider volumeSlider;

    public TMP_Dropdown resolutionDropdown;
    [SerializeField] private Text fullscreenText;
    private string saveKey = "mainMenuSettings";
    // Start is called before the first frame update
    void Start()
    {
        Load();
        controls_settings.SetActive(false);
        resolution_settings.SetActive(false);
        quality_settings.SetActive(false);
        sound_settings.SetActive(false);
        video1.SetActive(false);
        video2.SetActive(false);
        Debug.Log(width);
        Debug.Log(height);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controls_settings.SetActive(false);
            resolution_settings.SetActive(false);
            quality_settings.SetActive(false);
            sound_settings.SetActive(false);
            video1.SetActive(false);
            video2.SetActive(false);
        }

    }
    public void SetVolume()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
        Save();

        Debug.Log("sound is " + volume);
    }
    public void QuitGame()
    {
        Application.Quit();
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
        Debug.Log(currentQualityLevel);
    }

    public void ShowControls() 
    {
        controls_settings.SetActive(true);
        resolution_settings.SetActive(false);
        quality_settings.SetActive(false);
        sound_settings.SetActive(false);
        video1.SetActive(false);
        video2.SetActive(false);
    }
    public void ShowSound()
    {
        controls_settings.SetActive(false);
        resolution_settings.SetActive(false);
        quality_settings.SetActive(false);
        sound_settings.SetActive(true);
        video1.SetActive(false);
        video2.SetActive(false);
    }
    public void ShowVideo()
    {
        controls_settings.SetActive(false);
        resolution_settings.SetActive(false);
        quality_settings.SetActive(false);
        sound_settings.SetActive(false);
        video1.SetActive(true);
        video2.SetActive(true);
    }
    public void ShowQuality()
    {
        controls_settings.SetActive(false);
        resolution_settings.SetActive(false);
        quality_settings.SetActive(true);
        sound_settings.SetActive(false);

        lowQualityToggle.onValueChanged.AddListener(newValue => OnQualityToggleValueChanged(newValue, 0));
        mediumQualityToggle.onValueChanged.AddListener(newValue => OnQualityToggleValueChanged(newValue, 2));
        highQualityToggle.onValueChanged.AddListener(newValue => OnQualityToggleValueChanged(newValue, 5));
    }
    public void ShowResolution()
    {
        controls_settings.SetActive(false);
        resolution_settings.SetActive(true);
        quality_settings.SetActive(false);
        sound_settings.SetActive(false);

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutions.ToList());

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());

    }
    private void SetFull(bool status)
    {
        isFullScreen = status;
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
