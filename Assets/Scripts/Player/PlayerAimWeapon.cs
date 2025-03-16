using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    private Player _player;
    private Transform _aimTransform;
    private Vector3 _mousePosition;
    private Weapon _primaryWeapon;
    private Weapon _secondWeapon;
    private void Awake() {
        _aimTransform = transform.Find("Aim");
        _player = GetComponent<Player>();
    }
    private void Start() {
        _primaryWeapon = _aimTransform.GetComponentInChildren<AssaultRifle>();
        _secondWeapon = _aimTransform.GetComponentInChildren<GrenadeLauncher>();
        _primaryWeapon.Initialise();
        _secondWeapon.Initialise();
    }
    private void Update() {
        if(_player.IsStunned || _player.IsDead) return;
        _mousePosition = KeyboardWeaponInput.GetMousePosition(Camera.main);
        HandleAiming();
        HandleShooting();
    }
    private void HandleAiming()
    {
        Vector3 aimDirection = (_mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        _aimTransform.eulerAngles = new Vector3(0, 0, angle);
        Vector3 aimLocalScale = Vector3.one;
        if(angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
        }else
        {
            aimLocalScale.y = +1f;
        }
        _aimTransform.localScale = aimLocalScale;
    }

    private void HandleShooting()
    {
        if(_primaryWeapon != null && _primaryWeapon.HaveWeaponData())
        {
            _primaryWeapon.HandleInput(KeyboardWeaponInput.GetMousePosition(Camera.main));
        }
        if(_secondWeapon != null && _primaryWeapon.HaveWeaponData())
        {
            _secondWeapon.HandleInput(KeyboardWeaponInput.GetMousePosition(Camera.main));
        }
    }
}
