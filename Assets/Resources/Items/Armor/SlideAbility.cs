using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/SlideAbility")]
public class SlideAbility : Ability
{
    [SerializeField] private float slideSpeed;

    public override void UseAbility(Player player)
    {
        Slide(player);
    }

    public void Slide(Player player)
    {
        player.SetInvincible(1f);
        player.SetSlide(0.5f);
        player.SetAnimation("Slide", 0.5f);
        if (player.transform.lossyScale.x > 0)
        {
            player.velocity.x = slideSpeed;
        }
        else
        {
            player.velocity.x = -slideSpeed;
        }

    }
}
