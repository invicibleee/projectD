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
        // Ничего не делаем, так как эффект активируется при мигации через врагов
    }

    public void PerformBlinkstrike(Player player)
    {
        // Проверяем, активен ли чарм Blinkstrike Amulet
        if (isEffectActive)
        {
            // Определяем направление взгляда игрока
            int direction = player.facingDirection;

            // Получаем позицию центра для OverlapBoxAll
            Vector2 centerPosition = player.transform.position + new Vector3(dashOffset * direction, 0, 0);

            // Получаем список всех врагов, которых пересекает мигация игрока
            Collider2D[] colliders = Physics2D.OverlapBoxAll(centerPosition, new Vector2(dashX, dashY), 0f, LayerMask.GetMask("Enemy"));

            // Проходимся по всем пересеченным врагам
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
