using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _topPanel;
    [SerializeField]
    private GameObject _bottomPanel;
    [SerializeField]
    private GameObject _inventoryPanel;
    [SerializeField]
    private Slider _playerHPSlider;
    private void OnPlayerDataChanged(Player player)
    {
        _playerHPSlider.value = player.GetCurrentHealth()/player.GetMaxHealth();
    }
    public void Initialise(GameController gameController)
    {
        if(gameController == null) return;
        gameController.OnPlayerDataChanged += OnPlayerDataChanged;
        InitialiseTopPanel();
        InitialiseBottomPanel();
        InitialiseInventory();
    }
    private void InitialiseTopPanel()
    {
        if(_topPanel == null) return;
        if(_playerHPSlider != null)
        {
            _playerHPSlider.value = 1;
        }
    }
    private void InitialiseBottomPanel()
    {
        if(_bottomPanel == null) return;
    }
    private void InitialiseInventory()
    {
        if(_inventoryPanel == null) return;
    }
}
