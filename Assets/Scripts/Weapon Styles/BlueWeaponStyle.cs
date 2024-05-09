using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWeaponStyle : WeaponStyle
{
    public GameObject celestialNexusPrefab; // ������ ������� � �������� CelestialNexus

    public override void ActivateFirstUpgrade()
    {
        // ���������, ���������� �� ������
        if (celestialNexusPrefab != null)
        {
            // ������� ��������� ������� � �������� celestialNexusPrefab
            GameObject celestialNexusObject = Instantiate(celestialNexusPrefab, transform.position + transform.right * 2f, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Celestial Nexus prefab is not assigned!");
        }
    }

    // ��������������� ������ ��� ��������� ������� �������� �������� ����� ������
    public override void ActivateSecondUpgrade()
    {
    }

    // ��������������� ������ ��� ��������� �������� �������� �������� ����� ������
    public override void ActivateThirdUpgrade()
    {

    }
    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
    }
}
