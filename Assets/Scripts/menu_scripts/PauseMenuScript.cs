using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gui;
    public GameObject postProcessing;
    public GameObject map;

    public Button closeButton;

    public Button[] menuButtons;
    public GameObject[] menuPanels;

    public Button[] navigationButtons;
    public Text currentEssences;
    private int essenceAmount;
    private int increaseRate = 100;
    [SerializeField] private Text essenceText;

    private void Start()
    {
        // Initial state: menu is hidden
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
            gui.SetActive(true);
            postProcessing.SetActive(true);

        // Ensure the arrays are of the same length
        if (menuButtons.Length == menuPanels.Length && navigationButtons.Length == 2)
        {
            // Map buttons to their respective panels
            for (int i = 0; i < menuButtons.Length; i++)
            {
                int index = i; // Capture the current value of i
                menuButtons[i].onClick.AddListener(() => ShowPanel(menuPanels[index]));
            }

            navigationButtons[0].onClick.AddListener(ShowPreviousPanel);
            navigationButtons[1].onClick.AddListener(ShowNextPanel);

        }
        else
        {
            Debug.LogError("The number of buttons does not match the number of panels or navigation buttons are not set.");
        }
        essenceAmount = PlayerManager.instance.GetEssenceAmount();
        Debug.Log(essenceAmount);
        currentEssences.text = $"{essenceAmount}$";
        essenceText.text = $"{essenceAmount}$";

    }


    private void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (closeButton != null)
        {
            if (closeButton.onClick != null)
                closeButton.onClick.RemoveAllListeners();

            closeButton.onClick = new Button.ButtonClickedEvent();
            closeButton.onClick.AddListener(TogglePauseMenu);
        }
        essenceAmount = PlayerManager.instance.GetEssenceAmount();
        currentEssences.text = $"{essenceAmount}$";
        essenceText.text = $"{essenceAmount}$";
    }

    int SetEssenceAmount(int value) {
       int currentEssences = value;
        essenceText.text = currentEssences.ToString();
       return currentEssences;
    }
    public int GetEssenceAmount(int value)
    {
        value = essenceAmount;
        return value;
    }
    public void UseEssenceAmount(int amount)
    {
        essenceAmount -= amount;
        PlayerManager.instance.RemoveEssences(amount);
        SetEssenceAmount(essenceAmount);
        currentEssences.text = $"{essenceAmount}$";
    }

    // Method to toggle the visibility of the pause menu and control the game's pause state
    void TogglePauseMenu()
    {
        if (pauseMenu != null)
        {
            bool isMenuActive = pauseMenu.activeSelf;
            bool isGuiActive = gui.activeSelf;
            bool isPostActive = postProcessing.activeSelf;
            if(map != null)
            {
                bool isMapActive = map.activeSelf;
                map.SetActive(!isMapActive);
            }
            pauseMenu.SetActive(!isMenuActive);
            gui.SetActive(!isGuiActive);
            postProcessing.SetActive(!isPostActive);
           
            // Pause or resume the game based on the menu's visibility
            Time.timeScale = isMenuActive ? 1f : 0f;
        }

    }

    // Method to show a specific panel and hide others
    void ShowPanel(GameObject panelToShow)
    {
        foreach (var panel in menuPanels)
        {
            panel.SetActive(panel == panelToShow);
        }
    }

    // Method to show the previous panel
    void ShowPreviousPanel()
    {
        int currentIndex = GetCurrentPanelIndex();
        int previousIndex = (currentIndex - 1 + menuPanels.Length) % menuPanels.Length;
        ShowPanel(menuPanels[previousIndex]);
    }

    // Method to show the next panel
    void ShowNextPanel()
    {
        int currentIndex = GetCurrentPanelIndex();
        int nextIndex = (currentIndex + 1) % menuPanels.Length;
        ShowPanel(menuPanels[nextIndex]);
    }

    // Helper method to get the index of the currently active panel
    int GetCurrentPanelIndex()
    {
        for (int i = 0; i < menuPanels.Length; i++)
        {
            if (menuPanels[i].activeSelf)
            {
                return i;
            }
        }

        return -1; // Default value if no panel is active
    }

}
