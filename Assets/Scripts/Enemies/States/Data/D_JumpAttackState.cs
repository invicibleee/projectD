using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newJumpAttackStateData", menuName = "Data/State Data/Jump Attack State")]
public class D_JumpAttackState : ScriptableObject
{
    public Vector2 jumpAngle;
    public float jumpSpeed = 20.0f;
    public float jumpTime = 3f;
    public float jumpAttackCooldown = 10.0f;

}
