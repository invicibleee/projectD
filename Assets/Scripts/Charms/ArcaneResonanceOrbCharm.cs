using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneResonanceOrbCharm : Charm
{
    [SerializeField] private float radiusIncreasePercentage; // Процентное увеличение радиуса магических способностей

    private float originalMaxSize; // Исходное значение максимального размера хроносферы

    public override void ActivateEffect()
    {
        originalMaxSize = SkillManager.instance.chrono.GetMaxSize();

        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        SkillManager.instance.chrono.SetMaxSize(originalMaxSize);

        base.DeactivateEffect();
    }

    public override void CharmEffect()
    {
        float newSize = SkillManager.instance.chrono.GetMaxSize();
        newSize *= (1 + radiusIncreasePercentage / 100f);
        SkillManager.instance.chrono.SetMaxSize(newSize);
    }
}
