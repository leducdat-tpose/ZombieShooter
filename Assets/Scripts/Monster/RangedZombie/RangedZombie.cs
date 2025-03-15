using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedZombie : Monster
{
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _safeDistance;

    private void Start() {
        player = GameObject.FindGameObjectWithTag(Constant.PlayerTag).transform;
        // ChangeState(MonsterState.Chase);
    }
    protected override void AttackBehaviour()
    {
        if(currentAttackCoolDown > 0) return;
        Attack();
    }

    // protected override void Update()
    // {
    //     if(currentAttackCoolDown > 0)
    //     {
    //         currentAttackCoolDown -= Time.deltaTime;
    //     }

    //     base.Update();
    // }
    protected override void Attack()
    {
        // if(DistanceToPlayerIsSafe()) ChangeState(MonsterState.Chase);
        currentAttackCoolDown = monsterData.AttackCoolDown;
        GameObject projectile = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        projectile.SetActive(false);
        Bullet bullet = projectile.GetComponent<Bullet>();
        bullet.Initialise(player.position);
    }

    public override void Chasing()
    {
        if(DistanceToPlayerIsSafe())
        {
            base.Chasing();
        }
        else{
            // ChangeState(MonsterState.Attack);
            rigid.velocity = Vector2.zero;
        }
    }
    private bool DistanceToPlayerIsSafe()
    {
        if(Vector2.Distance(player.position, transform.position) > _safeDistance) return true;
        return false;
    }
}
