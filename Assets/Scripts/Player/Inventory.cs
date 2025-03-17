using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Player _player;
    private Dictionary<ItemData, int> _inventory;
    private Inventory(){
        _inventory = new Dictionary<ItemData, int>();
    }
    private void Initialise(Player player)
    {
        _player = player;
    }
    public static Inventory CreateAndInit(Player player)
    {
        Inventory inventory = new Inventory();
        inventory.Initialise(player);
        return inventory;
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
    public bool UseItem(ItemData item, int amount = 1)
    {
        if(!HaveThisItem(item)) return false;
        if(!item.CanPlayerUse) return false;
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
        while(taken != 0)
        {
            item.ItemFunction(_player.gameObject);
            taken--;
        }
        if(_inventory[item] == 0) _inventory.Remove(item);
        return true;
    }
    public int GetAmountItem(ItemData item, int amount = 1)
    {
        if(!HaveThisItem(item)) return 0;
        int taken = 0;
        Debug.Log($"Amount in inventory: {_inventory[item]}");
        if(_inventory[item] < amount)
        {
            Debug.Log($"First");
            taken = _inventory[item];
            _inventory[item] = 0;
        }
        else
        {
            Debug.Log($"Second");
            _inventory[item] -= amount;
            taken = amount;
        }
        Debug.Log($"Amount in inventory: {_inventory[item]}");
        if(_inventory[item] == 0) _inventory.Remove(item);
        return taken;
    }
    public bool HaveThisItem(ItemData item)
    {
        if(_inventory.ContainsKey(item)) return true;
        else return false;
    }
    public Dictionary<ItemData, int> GetData() => _inventory;
}
