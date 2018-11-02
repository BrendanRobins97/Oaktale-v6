using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DashAbility")]
public class DashAbility : Ability {

    [SerializeField] private float dashSpeed;

	public override void UseAbility(Player player)
    {
        Dash(player);
    }

    public void Dash(Player player)
    {
        player.SetInvincible(1f);
        if (player.transform.lossyScale.x > 0)
        {
            player.velocity.x = dashSpeed;
        } else
        {
            player.velocity.x = -dashSpeed;
        }

    }
}
