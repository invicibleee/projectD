using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCollision : MonoBehaviour
{
    private bool isPlayerNearby = false;
    [SerializeField] private Text message;
    [SerializeField] private string text;
   
    [SerializeField] private int sceneIndex;
    private Vector2 statuePosition;

    private bool activated;

    private string saveKey = "StatueSave";

    private void Awake()
    {
        statuePosition= transform.position;
    }
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Save();
            activated = true;
            message.text = "";
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
