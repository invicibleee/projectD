using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newDashStateData", menuName = "Data/State Data/Dash State")]
public class D_DashState : ScriptableObject
{
    public float dashSpeed = 3.0f;
    public float dashCooldown = 5f;
    public float dashTime = 0.5f;
    public float normalizedTime = 0f;
    public float dashDistance = 5f;
    public float attackRadius = 0.8f;
    public float attackDamage = 10f;
    public Vector2 dashDirection;
    public LayerMask whatIsPlayer;

}
