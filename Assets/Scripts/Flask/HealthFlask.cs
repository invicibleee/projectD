using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class HealthFlask : MonoBehaviour
{
    [SerializeField] private int healthAmount = 50; // ���������� ������������������ ��������
    [SerializeField] private int currentFlasks;
    [SerializeField] private int maxFlasks;
    public bool canUseFlasks = true;

    public FlaskGUI flaskGUI;
    private PlayerStats playerStats;
    private void Start()
    {
        currentFlasks = maxFlasks;
        flaskGUI = FindObjectOfType<FlaskGUI>();
        playerStats = FindObjectOfType<PlayerStats>();
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
}
