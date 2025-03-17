using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private ItemData _requireKeyData;
    [SerializeField]
    private bool _notRequiredKey;
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag(Constant.PlayerTag)) return;
        if(!_notRequiredKey)
        {
            Player player = other.GetComponent<Player>();
            if(!player.Inventory.HaveThisItem(_requireKeyData)) return;
            player.Inventory.GetAmountItem(_requireKeyData);
        }
        Destroy(this.gameObject);
    }
}