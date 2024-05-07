using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlinkstrikeAmuletCharm : Charm
{
    private bool isEffectActive = false;
    [SerializeField] private float dashX; 
    [SerializeField] private float dashY;
    [SerializeField] private float dashOffset;

    public override void ActivateEffect()
    {
        isEffectActive = true;
    }

    public override void DeactivateEffect()
    {
        isEffectActive = false;
    }

    public override void CharmEffect()
    {
        // ������ �� ������, ��� ��� ������ ������������ ��� ������� ����� ������
    }

    public void PerformBlinkstrike(Player player)
    {
        // ���������, ������� �� ���� Blinkstrike Amulet
        if (isEffectActive)
        {
            // ���������� ����������� ������� ������
            int direction = player.facingDirection;

            // �������� ������� ������ ��� OverlapBoxAll
            Vector2 centerPosition = player.transform.position + new Vector3(dashOffset * direction, 0, 0);

            // �������� ������ ���� ������, ������� ���������� ������� ������
            Collider2D[] colliders = Physics2D.OverlapBoxAll(centerPosition, new Vector2(dashX, dashY), 0f, LayerMask.GetMask("Enemy"));

            // ���������� �� ���� ������������ ������
            foreach (var collider in colliders)
            {
                EnemyStats enemyStats = collider.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(player.stats.damage.GetValue());
                }
            }
        }
    }



}
