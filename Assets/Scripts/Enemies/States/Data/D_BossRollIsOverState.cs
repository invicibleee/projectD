using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newRollOverData", menuName = "Data/State Data/Boss Roll Over Data")]
public class D_BossRollIsOverState : ScriptableObject
{
    public float minTimetOverRoll = 0.5f;
    public float maxTimeOverRoll = 3.0f;
}
