using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField]
    private GameObject _topPanel;
    [SerializeField]
    private GameObject _bottomPanel;
    [SerializeField]
    private GameObject _inventoryPanel;
    [SerializeField]
    private Slider _playerHPSlider;
    [Header("Button")]
    [SerializeField]
    private Button _inventoryBtn;
    private GameController _gameController;
    private void OnPlayerDataChanged(Player player)
    {
        _playerHPSlider.value = player.GetCurrentHealth()/player.GetMaxHealth();
    }
    public void Initialise(GameController gameController)
    {
        if(gameController == null) return;
        _gameController = gameController;
        gameController.OnPlayerDataChanged += OnPlayerDataChanged;
        InitialiseTopPanel();
        InitialiseBottomPanel();
        InitialiseInventory(_gameController);
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
    private void InitialiseInventory(GameController gameController)
    {
        if(_inventoryPanel == null) return;
        _inventoryPanel.SetActive(false);
        if(_inventoryPanel.TryGetComponent<InventoryUI>(out InventoryUI inventoryUI))
        {
            _inventoryBtn.onClick.AddListener(() => _inventoryPanel.SetActive(true));
            inventoryUI.Initialise(gameController);
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(!_inventoryPanel.activeSelf)
            {
                _inventoryPanel.SetActive(true);
            }else
            {
                _inventoryPanel.SetActive(false);
                _gameController.UnSelectItem();
            }
        }
    }
}
