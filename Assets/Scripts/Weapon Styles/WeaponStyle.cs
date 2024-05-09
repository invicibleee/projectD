using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStyle : MonoBehaviour
{
    public int styleIndex;
    public virtual void ActivateFirstUpgrade()
    {
    }

    // ���������� ������ ������� ����� ������
    public virtual void ActivateSecondUpgrade()
    {

    }

    // ���������� ������ ������� ����� ������
    public virtual void ActivateThirdUpgrade()
    {

    }

    // ��������� ������� ����� ������
    public virtual void DeactivateEffect()
    {
    }
}
