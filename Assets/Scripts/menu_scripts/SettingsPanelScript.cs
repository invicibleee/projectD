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

    private bool isFullScreen = true;
    private int currentQualityLevel = 2;

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

    // Start is called before the first frame update
    void Start()
    {
        controls_settings.SetActive(false);
        resolution_settings.SetActive(false);
        quality_settings.SetActive(false);
        sound_settings.SetActive(false);
        video1.SetActive(false);
        video2.SetActive(false);
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
        Debug.Log("sound is " + volume);
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnResolutionChanged(int resolutionIndex)
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
        Debug.Log(isFullScreen);
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
            mediumQualityToggle.isOn = true;
            currentQualityLevel = 2;
            QualitySettings.SetQualityLevel(currentQualityLevel);
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
}
