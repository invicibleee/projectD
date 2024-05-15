using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charm : MonoBehaviour
{
   [SerializeField] protected PlayerStats playerStats;

    // ����� ��� ��������� �����
    public virtual void ActivateEffect()
    {
        CharmEffect();
    }

    // ����� ��� ���������� �����
    public virtual void DeactivateEffect()
    {
    }

    public abstract void CharmEffect();
}
