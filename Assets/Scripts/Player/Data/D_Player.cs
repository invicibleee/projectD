using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/PlayerState Data/Player Data")]
public class D_Player : ScriptableObject
{
    public LayerMask whatIsEnemy;
    public float maxHealt = 100f;

    public float damageHopSpeed = 10.0f;

}
