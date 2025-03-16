using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private ItemData _requireKeyData;
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag(Constant.PlayerTag)) return;
        Player player = other.GetComponent<Player>();
        if(!player.Inventory.HaveThisItem(_requireKeyData)) return;
        player.Inventory.GetAmountItem(_requireKeyData);
        Destroy(this.gameObject);
    }
}