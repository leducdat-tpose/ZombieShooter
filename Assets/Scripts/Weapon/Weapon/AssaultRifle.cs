using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class AssaultRifle : Weapon
{
    private bool _isShooting;
    public override void Fire(Vector3 position, bool noneReload = false)
    {
        if(noneReload == false)
        {
            nextFireTime = Time.time + (1f/weaponData.FireRate);
            currentAmmo--;
        }
        // GameObject projectile = Instantiate(weaponData.ProjectilePrefab, transform.position, Quaternion.identity);
        GameObject projectile = ObjectPool.Instance.GetObject(weaponData.ProjectilePrefab, transform.position, Quaternion.identity);
        if(projectile.TryGetComponent<StraightProjectile>(out StraightProjectile component))
        {
            // Vector3 mousePos = KeyboardWeaponInput.GetMousePosition(Camera.main);
            Vector2 direction = (position - transform.position).normalized;
            component.Initialise(weaponData.Damage, direction);
        }
    }

    public override void HandleInput(Vector3 position, bool noneReload = false)
    {
        if(noneReload == true)
        {
            Fire(position, noneReload); 
            return;
        }
        _isShooting = KeyboardWeaponInput.PrimaryIsFiring();
        if(KeyboardWeaponInput.IsReloading() || currentAmmo == 0) Reload();
        if(_isShooting && Time.time > nextFireTime && !isReloading)
        {
            Fire(position, noneReload);
        }
    }
    

    public override void Initialise()
    {
        if(!HaveWeaponData()) return;
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
