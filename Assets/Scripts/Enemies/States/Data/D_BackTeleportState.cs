using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newTeleportStateData", menuName = "Data/State Data/Teleport State")]
public class D_BackTeleportState : ScriptableObject
{
    public float teleportTime = 0.5f;
    public float teleportCooldown = 3.0f;
    public float distanceBehindPlayer = 3.0f;
    public LayerMask whatIsPlayer;
}
