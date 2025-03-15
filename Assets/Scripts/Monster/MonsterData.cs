using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName ="Monster Data/MonsterData")]
public class MonsterData : ScriptableObject
{
    public float Health;
    public float MoveSpeed;
    public float Damage;
    public float AttackCoolDown;
    public float AttackRange;
    public float DetectionRange;
}