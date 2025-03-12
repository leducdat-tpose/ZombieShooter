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
    public abstract void Fire();
    public abstract void Reload();
    public abstract void HandleInput();
}
