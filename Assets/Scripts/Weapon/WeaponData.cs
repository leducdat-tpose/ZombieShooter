using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="WeaponData", menuName ="ItemData/Weapon Data")]
public class WeaponData : ItemData
{
    public GameObject WeaponPrefab;
    public GameObject ProjectilePrefab;
    public float Damage;
    public float FireRate;
    public float ReloadDuration;
    public int AmmoCapacityPerMagazine = 10;
    public ItemData AmmoType;
}