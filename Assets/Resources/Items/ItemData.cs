// File: ItemData.cs
// Author: Brendan Robinson
// Date Created: 01/04/2018
// Date Last Modified: 07/15/2018
// Description: 

using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class ItemData : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public int id;
    public int stackSize;

    // Use 1 is left click, the main use
    public virtual void Use1(Player player)
    {
    }

    // Use 2 is right click, the special use
    public virtual void Use2(Player player)
    {
    }

    public virtual void Activate(Player player)
    {
    }

    public virtual void Deactivate(Player player)
    {
        foreach (Transform child in player.equip.transform) Destroy(child.gameObject);
    }

    public virtual void Delete(int x, int y)
    {
        SimplePool.Spawn(GameManager.Get<Prefabs>().pickup, new Vector2(x + 0.5f, y + 0.5f), Quaternion.identity)
            .GetComponent<Pickup>().Initialize(new Item(id), 1);
    }
}