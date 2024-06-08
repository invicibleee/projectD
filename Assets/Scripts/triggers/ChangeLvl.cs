using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLvl : MonoBehaviour
{
    [SerializeField] Vector2 position;
    [SerializeField] int scene;
    [SerializeField] private ScreenFade screenFade;
    [SerializeField] GameObject ui;
    [SerializeField] private int precentage;
    private CharacterPanelScript characterPanel;
    private string saveKey = "playerPosition";

    private void Start()
    {
        ui.SetActive(false);
        characterPanel = FindObjectOfType<CharacterPanelScript>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (screenFade != null)
            {
                Save();
                ui.SetActive(true);
                if(characterPanel.completionPercentage < precentage)
                {
                    characterPanel.SetProgress(precentage);
                }
                screenFade.FadeOutAndChangeScene(scene); 
            }

        }
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());

    }

    private SaveData.PlayerPos GetData()
    {
        var data = new SaveData.PlayerPos()
        {
            _playerPos = position,
        };
        return data;
    }
}
