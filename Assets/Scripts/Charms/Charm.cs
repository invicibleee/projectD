using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charm : MonoBehaviour
{
   [SerializeField] protected PlayerStats playerStats;

    // Метод для включения чарма
    public virtual void ActivateEffect()
    {
        CharmEffect();
    }

    // Метод для отключения чарма
    public virtual void DeactivateEffect()
    {
    }

    public abstract void CharmEffect();
}
