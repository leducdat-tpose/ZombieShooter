using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="WeaponData", menuName ="ScriptableObject/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string WeaponName;
    public GameObject WeaponPrefab;
    public GameObject ProjectilePrefab;
    public float Damage;
    public float FireRate;
    public float ReloadDuration;
    public int AmmoCapacityPerMagazine = 10;
}