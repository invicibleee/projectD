using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newLazerAttackStateData", menuName = "Data/State Data/Lazer Attack State")]
public class D_LazerAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10.0f;

}
