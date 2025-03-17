using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="WeaponData", menuName ="ItemData/Weapon Data")]
public class WeaponData : ItemData
{
    public enum SpecialAbility{
        None,
        Stun,
    }
    public GameObject WeaponPrefab;
    public GameObject ProjectilePrefab;
    public float Damage;
    public float FireRate;
    public SpecialAbility Ability;
    public float ReloadDuration;
    public int AmmoCapacityPerMagazine = 10;
    public ItemData AmmoType;
}