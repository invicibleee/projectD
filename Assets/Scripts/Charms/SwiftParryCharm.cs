using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftParryCharm : Charm
{
    private Player player;
    [SerializeField] private float dashSpeedIncreasePercent;
    [SerializeField] private float dashDurationIncreasePercent;

    private float originalSpeed;
    private float originalDuration;
    private void Start()
    {
        player = FindObjectOfType<Player>();

        originalSpeed = player.dashSpeed;
        originalDuration = player.dashDuration;
    }
    public override void ActivateEffect()
    {
        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
        player.dashSpeed = originalSpeed;
        player.dashDuration = originalDuration;

    }
    public override void CharmEffect()
    {
        player.dashSpeed += player.dashSpeed * dashSpeedIncreasePercent / 100;
        player.dashDuration += player.dashDuration * dashDurationIncreasePercent / 100;
    }
}
