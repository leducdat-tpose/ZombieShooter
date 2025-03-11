using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeZombie : Monster
{
    private void Start() {
        player = GameObject.FindGameObjectWithTag(Constant.PlayerTag).transform;
        ChangeState(MonsterState.Chase);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(Constant.PlayerTag))
        {
            Debug.Log(player);
        }
    }
}
