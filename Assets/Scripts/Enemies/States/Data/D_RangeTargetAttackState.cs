using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newTargetRangeAttackStateData", menuName = "Data/State Data/Target Range Attack State")]
public class D_RangeTargetAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10.0f;
    public float projectileSpeed = 10.0f;
    public float projectileTravelDistance = 10.0f;
    public float pursuitTime = 2.0f;
    public float timeToDestroy = 3.0f;
}
