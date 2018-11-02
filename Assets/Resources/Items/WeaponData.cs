using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 1)]
public class WeaponData : ItemData {

    public GameObject weapon;

    private Weapon wep;

    public override void Use1(Player player)
    {
        wep.Attack();
    }

    public override void Use2(Player player)
    {
        wep.SpecialAttack();
    }

    public override void Activate(Player player)
    {                                                                                                           
        Instantiate(weapon, player.equip.transform);
        wep = player.equip.GetComponentInChildren<Weapon>();
        if (player.currentItem.id != 0) {
            Debug.Log(player.currentItem.id);
            WeaponItem wepItem = (WeaponItem)player.currentItem;
            wep.experience = wepItem.experience;

        }
    }

}
