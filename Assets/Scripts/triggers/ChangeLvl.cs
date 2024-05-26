using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLvl : MonoBehaviour
{
    [SerializeField] Vector2 position;
    [SerializeField] int scene;
    [SerializeField] private ScreenFade screenFade;
    [SerializeField] GameObject ui;

    private string saveKey = "playerPosition";

    private void Start()
    {
        ui.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (screenFade != null)
            {
                Save();
                ui.SetActive(true);
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
