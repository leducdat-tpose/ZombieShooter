using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Button _closeBtn;
    [SerializeField]
    private Button _useBtn;
    [SerializeField]
    private GameObject _gridPanel;
    private List<ItemSlot> _itemSlots = new List<ItemSlot>();
    private GameController _gameController;
    public void Initialise(GameController gameController)
    {
        _gameController = gameController;
        _closeBtn.onClick.AddListener(() =>{
            TurnOffInventoryUI();
        });
        _useBtn.onClick.AddListener(() => {
            _gameController.HandleItem();
            ResetItemSlot();
            SetupItemSlot();
        });
        foreach(Transform transform in _gridPanel.transform)
        {
            if(transform.TryGetComponent<ItemSlot>(out ItemSlot slot))
            {
                _itemSlots.Add(slot);
                slot.Initialise(_gameController);
                slot.gameObject.SetActive(false);
            }
        }
    }
    private void SetupItemSlot()
    {
        int i = 0;
        foreach(KeyValuePair<ItemData, int> item in _gameController.GetPlayer().Inventory.GetData())
        {
            _itemSlots[i].gameObject.SetActive(true);
            _itemSlots[i].SetupItemSlot(item.Key, item.Value);
            i++;
        }
    }

    private void ResetItemSlot()
    {
        foreach(ItemSlot slot in _itemSlots)
        {
            if(!slot.gameObject.activeSelf) break;
            slot.gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        SetupItemSlot();
    }
    private void OnDisable() {
        ResetItemSlot();
    }
    public void TurnOffInventoryUI()
    {
        _gameController.UnSelectItem();
        this.gameObject.SetActive(false);
    }
}