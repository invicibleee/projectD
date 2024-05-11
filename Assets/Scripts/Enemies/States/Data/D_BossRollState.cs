using UnityEngine;

[CreateAssetMenu(fileName = "newRollStateData", menuName = "Data/State Data/Roll State")]
public class D_BossRollState : ScriptableObject
{
    public float rollSpeed = 20.0f; 
    public float minRollTime = 1f;
    public float maxRollTime = 2f;
    public float rollCooldown = 5.0f;
    public float angleTime = 0.5f;
    public float attackRadius = 0.8f;
    public LayerMask whatIsPlayer;
    public Vector2 rollAngle;
}
