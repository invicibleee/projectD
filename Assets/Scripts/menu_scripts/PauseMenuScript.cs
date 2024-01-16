using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gui;

    public Button closeButton;

    public Button[] menuButtons;
    public GameObject[] menuPanels;

    public Button[] navigationButtons;
    public Text currency;
    private int Currency;

    private void Start()
    {
        // Initial state: menu is hidden
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
            gui.SetActive(true);

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
        Currency = SetCurrency(1000);
        currency.text = $"{Currency}$";
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
    }
    int SetCurrency(int value) {
       int currency = value;
       return currency;
    }
    public int GetCurrency(int value)
    {
        value = Currency;
        return value;
    }
    public void UseCurrency(int amount)
    {
        Currency -= amount;
        SetCurrency(Currency);
        currency.text = $"{Currency}$";
    }

    // Method to toggle the visibility of the pause menu and control the game's pause state
    void TogglePauseMenu()
    {
        if (pauseMenu != null)
        {
            bool isMenuActive = pauseMenu.activeSelf;
            bool isGuiActive = gui.activeSelf;
            pauseMenu.SetActive(!isMenuActive);
            gui.SetActive(!isGuiActive);
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
