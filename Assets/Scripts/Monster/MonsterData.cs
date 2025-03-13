using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName ="Monster Data/MonsterData")]
public class MonsterData : ScriptableObject
{
    public float Health;
    public float MoveSpeed;
    public float AttackCoolDown;
}