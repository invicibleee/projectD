using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCollision : MonoBehaviour
{
    private bool isPlayerNearby = false;

    private HealthFlask healthFlask;
    private Player player;
    private BarsController bars;
    public FlaskGUI flaskGUI;

    [SerializeField] private int sceneIndex;
    [SerializeField] private Text message;
    [SerializeField] private string text;

    private Vector2 statuePosition;

    private bool activated;

    private string saveKey = "StatueSave";

    private void Awake()
    {
        statuePosition= transform.position;
        healthFlask = FindAnyObjectByType<HealthFlask>();
        player = FindAnyObjectByType<Player>();
        bars = FindAnyObjectByType<BarsController>();
        flaskGUI= FindAnyObjectByType<FlaskGUI>();
    }
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Save();
            activated = true;
            message.text = "";

        }
        if (activated)
        {
            player.stats.currentHealth = player.stats.maxHealth.GetValue();
            healthFlask.currentFlasks = healthFlask.maxFlasks;
            bars.SetHealth(player.stats.currentHealth, player.stats.maxHealth.GetValue());
            flaskGUI.FillImage(0);
            flaskGUI.FillImage(1);
            flaskGUI.FillImage(2);
            activated = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (!activated)
            {
                message.text = text;
            }
            else
            {
                message.text = "";
            }
          
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            message.text = "";
        }
    }

    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }

    private SaveData.StatueSave GetData()
    {
        var data = new SaveData.StatueSave()
        {
            _sceneIndex= sceneIndex,
            _statuePosition = statuePosition,
        };

        return data;
    }
}
