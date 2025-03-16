using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController
{
    private Player _player;
    public event Action<Player> OnPlayerDataChanged;
    private ItemData _selectedItem;
    
    private GameController(){}
    ~GameController()
    {
        if(_player != null) _player.OnPlayerDataChanged -= PlayerDataChanged;
    }
    private void Initialise(Player player)
    {
        _player = player;
        _player.OnPlayerDataChanged += PlayerDataChanged;
    }
    public static GameController CreateAndInit(Player player)
    {
        if(player == null) return null;
        GameController gameController = new GameController();
        gameController.Initialise(player);
        return gameController;
    }
    public void PlayerDataChanged()
    {
        OnPlayerDataChanged?.Invoke(_player);
    }
    public void SelectItem(ItemData selectedItem)
    {
        Debug.Log($"Selected Item: {selectedItem}");
        _selectedItem = selectedItem;
    }
    public void UnSelectItem() => _selectedItem = null;
    public bool HandleItem()
    {
        if(_selectedItem == null) return false;
        if(!_player.Inventory.HaveThisItem(_selectedItem)) return false;
        bool result = _player.Inventory.UseItem(_selectedItem);
        if(result)
        {
            PlayerDataChanged();
        }
        return result;
    }
    public Player GetPlayer() => _player;
}