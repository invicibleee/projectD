using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChageData", menuName = "Data/State Data/Chage Data")]
public class D_ChargeState : ScriptableObject
{
    public float chargeSpeed = 6f;

    public float chargeTime = 2f;
}
