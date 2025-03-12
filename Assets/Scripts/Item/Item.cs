using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemData _itemData;
    [SerializeField]
    private int _amount = 1;
    public ItemData GetData() => _itemData;
    public int GetAmount() => _amount;
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag(Constant.PlayerTag)) return;
        Player player = other.GetComponent<Player>();
        player.Inventory.AddItem(_itemData, _amount);
        Destroy(this.gameObject);
    }
}
