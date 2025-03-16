using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    [SerializeField]
    private AnimationCurve _pathCurve;
    [SerializeField]
    private AnimationCurve _axisCorrectionCurve;
    [SerializeField]
    private AnimationCurve _speedCurve;
    [SerializeField]
    private float _maxRelativeHeight;
    public override void Fire(Vector3 position, bool noneReload = false)
    {
        if(noneReload == false)
        {
            nextFireTime = Time.time + (1f/weaponData.FireRate);
            currentAmmo--;
        }
        // GameObject projectile = Instantiate(weaponData.ProjectilePrefab, transform.position, Quaternion.identity);
        GameObject projectile = ObjectPool.Instance.GetObject(weaponData.ProjectilePrefab, transform.position, Quaternion.identity);
        if(projectile.TryGetComponent<ParabolicProjectile>(out ParabolicProjectile component))
        {
            // Vector3 mousePos = KeyboardWeaponInput.GetMousePosition(Camera.main);
            component.Initialise(weaponData.Damage, position);
            component.SetCurves(_pathCurve, _axisCorrectionCurve, _speedCurve, _maxRelativeHeight);
        }
    }

    public override void HandleInput(Vector3 position, bool noneReload = false)
    {
        if(noneReload == true)
        {
            Fire(position, noneReload);
        }
        if(currentAmmo == 0) Reload();
        if(KeyboardWeaponInput.SecondIsFiringReleased() && Time.time > nextFireTime && !isReloading)
        {
            Fire(position, noneReload);
        }
    }

    public override void Initialise()
    {
        if(!HaveWeaponData()) return;
        // GameObject.Instantiate(weaponData.WeaponPrefab, parent: transform);
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
