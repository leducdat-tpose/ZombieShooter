using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class AssaultRifle : Weapon
{
    private bool _isShooting;
    public override void Fire()
    {
        nextFireTime = Time.time + (1f/weaponData.FireRate);
        currentAmmo--;
        GameObject projectile = Instantiate(weaponData.ProjectilePrefab, transform.position, Quaternion.identity);
        if(projectile.TryGetComponent<StraightProjectile>(out StraightProjectile component))
        {
            Vector3 mousePos = KeyboardWeaponInput.GetMousePosition(Camera.main);
            Vector2 direction = (mousePos - transform.position).normalized;
            component.Initialise(weaponData.Damage, direction);
        }
        Destroy(projectile, 3f);
    }

    public override void HandleInput()
    {
        _isShooting = KeyboardWeaponInput.IsFiring();
        if(KeyboardWeaponInput.IsReloading() || currentAmmo == 0) Reload();
        if(_isShooting && Time.time > nextFireTime && !isReloading)
        {
            Fire();
        }
    }

    public override void Initialise()
    {
        GameObject.Instantiate(weaponData.WeaponPrefab, parent: transform);
        currentAmmo = weaponData.AmmoCapacityPerMagazine;
    }

    public override void Reload()
    {
        if(isReloading) return;
        StartCoroutine(ReloadCoroutine());
    }
    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(weaponData.ReloadDuration);
        currentAmmo = weaponData.AmmoCapacityPerMagazine;
        isReloading = false;
    }
}
