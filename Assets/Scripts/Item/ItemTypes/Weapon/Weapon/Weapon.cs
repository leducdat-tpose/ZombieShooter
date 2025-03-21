using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponData weaponData;
    protected float nextFireTime;
    protected bool isReloading;
    protected int currentAmmo;
    public abstract void Initialise();
    public abstract void Fire(Vector3 position, bool noneReload = false);
    public abstract void Reload(int amount);
    public abstract void HandleInput(Vector3 position, bool noneReload = false);
    public bool IsReloading() => isReloading;
    public bool HaveWeaponData() => weaponData;
    public int GetCurrentAmmo() => currentAmmo;
    public ItemData GetAmmoType() => weaponData.AmmoType;
    public int GetMaxAmmo() => weaponData.AmmoCapacityPerMagazine;
}
