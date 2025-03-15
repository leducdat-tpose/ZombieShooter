using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAimWeapon : MonoBehaviour
{
    private Monster _monster;
    private Transform _aimTransform;
    private Weapon _weapon;
    // Start is called before the first frame update
    private void Awake() {
        _aimTransform = transform.Find("Aim");
        _monster = GetComponent<Monster>();
    }
    void Start()
    {
        _weapon = _aimTransform.GetComponentInChildren<Weapon>();
        _weapon.Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        if(_monster.TargetTransform == null) return;
        HandleAiming();
    }
    private void HandleAiming()
    {
        Vector3 aimDirection = (_monster.TargetTransform.position - transform.position).normalized;
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

    public void HandleShooting()
    {
        _weapon.HandleInput(_monster.TargetTransform.position, noneReload: true);
    }
}
