using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newJumpAttackDoneStateData", menuName = "Data/State Data/Jump Attack Done State")]

public class D_JumpAttackDone : ScriptableObject
{
    public float attackRadius = 0.8f;
    public LayerMask whatIsPlayer;
}
