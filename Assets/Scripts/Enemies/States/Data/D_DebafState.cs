using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newDebafData", menuName = "Data/State Data/Debaf Data")]

public class D_DebafState : ScriptableObject
{
    public float debafSpeedProcent = 30.0f;
    public float debafDuration = 5.0f;
    public float debafCooldown = 10.0f;
}
