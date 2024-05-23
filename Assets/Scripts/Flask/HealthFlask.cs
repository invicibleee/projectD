using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class HealthFlask : MonoBehaviour
{
    [SerializeField] private int healthAmount = 50; // ���������� ������������������ ��������
    [SerializeField] public int currentFlasks;
    [SerializeField] public int maxFlasks;
    public bool canUseFlasks = true;

    public FlaskGUI flaskGUI;
    private PlayerStats playerStats;
    private string saveKey = "flasks";
    private void Start()
    {
       
        flaskGUI = FindObjectOfType<FlaskGUI>();
        playerStats = FindObjectOfType<PlayerStats>();
        Load();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && canUseFlasks)
        {
            UseFlask();
        }
    }
    public void UseFlask()
    {
        // ���������, ���� �� � ������ ��������� ������
        if (currentFlasks > 0 && currentFlasks <= maxFlasks)
        {
            // ��������������� �������� ������
            playerStats.IncreaseHealthBy(healthAmount);
            // ��������� ���������� ������
            currentFlasks--;
            if (flaskGUI != null)
            {
                // ���� ���� ����������� ������
                if (flaskGUI.images.Length > currentFlasks)
                {
                    flaskGUI.ClearImage(currentFlasks);
                    Save();
                }
            }
        } else
        {
            Debug.Log("asd");
        }
    }
    public void CanUseFlasks(bool able)
    {
        canUseFlasks = able;
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());

    }
    public void Load()
    {
        var data = SaveManager.Load<SaveData.CharaStatistic>(saveKey);
        currentFlasks = data._currentFlask;
        maxFlasks = data._maxFlask;

        // ������� �� ����������
        for (int i = 0; i < flaskGUI.images.Length; i++)
        {
            flaskGUI.ClearImage(i);
        }

        // ���������� ���������� �������� �� ������� �������� ����
        for (int i = 0; i < currentFlasks; i++)
        {
            flaskGUI.FillImage(i);
        }

    }
    private SaveData.CharaStatistic GetData()
    {
        var data = new SaveData.CharaStatistic()
        {
            _currentFlask = currentFlasks,
            _maxFlask = maxFlasks,  
        };
        return data;
    }
}
