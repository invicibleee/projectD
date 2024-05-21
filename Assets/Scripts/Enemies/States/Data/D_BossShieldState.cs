using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newBossShieldStateData", menuName = "Data/State Data/Boss Shield State Data")]
public class D_BossShieldState : ScriptableObject
{
    public float minShieldTime = 1f;
    public float maxShieldTime = 5f;
    public float armotboost = 10.0f;
}
