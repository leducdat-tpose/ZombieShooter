using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerhitbox : MonoBehaviour
{
    private Player _player;
    private void Awake() {
        _player = transform.root.GetComponent<Player>();
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag(Constant.MonsterTag))
        {
            _player.TakeDamage(10);
        }
    }
}
