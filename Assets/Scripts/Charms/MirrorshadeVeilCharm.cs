using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorshadeVeilCharm : Charm
{
    public override void ActivateEffect()
    {
        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
        if (playerStats != null)
        {
            PlayerDashState.OnDashStart -= GrantInvincibility;
            PlayerDashState.OnDashEnd -= RemoveInvincibility;
        }
    }

    private void GrantInvincibility()
    {
        playerStats.MakeInvincible(true);
    }

    private void RemoveInvincibility()
    {
        playerStats.MakeInvincible(false);
    }

    public override void CharmEffect()
    {
        PlayerDashState.OnDashStart += GrantInvincibility;
        PlayerDashState.OnDashEnd += RemoveInvincibility;
    }
}
