using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected ItemData _itemData;
    [SerializeField]
    protected int _amount = 1;
    private void Awake() {
        GetComponent<SpriteRenderer>().sprite = _itemData.Sprite;
    }
    public ItemData GetData() => _itemData;
    public int GetAmount() => _amount;
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag(Constant.PlayerTag)) return;
        Player player = other.GetComponent<Player>();
        player.Inventory.AddItem(_itemData, _amount);
        _itemData.ItemFunction(player.gameObject);
        Destroy(this.gameObject);
    }
}