using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform _aimTransform;
    private Vector3 _mousePosition;
    private void Awake() {
        _aimTransform = transform.Find("Aim");
    }
    private void Update() {
        _mousePosition = Constant.GetMousePosition(Camera.main);
        HandleAiming();
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
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shoot");
        }
    }
}
