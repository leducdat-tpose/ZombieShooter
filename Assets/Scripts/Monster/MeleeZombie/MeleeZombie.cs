using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeZombie : Monster
{
    private void Start() {
        player = GameObject.FindGameObjectWithTag(Constant.PlayerTag).transform;
        ChangeState(MonsterState.Chase);
    }
}
