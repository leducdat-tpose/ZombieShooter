using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Dictionary<ItemData, int> _inventory;
    public Inventory(){
        _inventory = new Dictionary<ItemData, int>();
    }
    public void AddItem(ItemData item, int amount)
    {
        Debug.Log($"Adding function, {item.ItemName}, {amount}");
        if(!HaveThisItem(item))
        {
            _inventory[item] = 0;
        }
        _inventory[item] += amount;
    }
    public int UseItem(ItemData item, int amount = 1)
    {
        if(!HaveThisItem(item)) return 0;
        int taken = 0;
        if(_inventory[item] < amount)
        {
            taken = _inventory[item];
            _inventory[item] = 0;
        }
        else
        {
            _inventory[item] -= amount;
            taken = amount;
        }
        if(_inventory[item] == 0) _inventory.Remove(item);
        return taken;
    }
    public bool HaveThisItem(ItemData item) => _inventory.ContainsKey(item);
    public Dictionary<ItemData, int> GetData() => _inventory;
}
