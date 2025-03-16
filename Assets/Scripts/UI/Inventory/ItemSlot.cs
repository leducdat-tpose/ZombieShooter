using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private GameController _gameController;
    private ItemData _itemData;
    [SerializeField]
    private Button _btn;
    [SerializeField]
    private Image _itemImg;
    [SerializeField]
    private TextMeshProUGUI _amountText;
    public void Initialise(GameController gameController)
    {
        _gameController = gameController;
    }
    public void SetupItemSlot(ItemData itemData, int amount)
    {
        _itemData = itemData;
        _btn.onClick.AddListener(() => {
            _gameController.SelectItem(_itemData);
        });
        _itemImg.sprite = _itemData.Sprite;
        _amountText.text = $"{amount}";
    }
    private void OnDisable() {
        ResetSlot();
    }

    private void ResetSlot()
    {
        _itemImg.sprite = null;
        _itemData = null;
        _amountText.text =  $"{0}";
        _btn.onClick.RemoveAllListeners();
    }
}
