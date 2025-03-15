using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class RangedZombie : Monster
{
    private MonsterAimWeapon _aimWeapon;
    protected override void Start() {
        base.Start();
        _aimWeapon = GetComponent<MonsterAimWeapon>();
        stateManager = new StateManager<Monster>();
        stateManager.AddState(new IdleStateZombie(this, stateManager));
        stateManager.AddState(new ChaseStateZombie(this, stateManager));
        stateManager.AddState(new AttackStateZombie(this, stateManager));
        stateManager.AddState(new DeathStateZombie(this, stateManager));
        stateManager.ChangeState<IdleStateZombie>();
    }
    private void Update() {
        stateManager.Update();
    }
    private void FixedUpdate() {
        stateManager.FixedUpdate();
    }
    public override bool HasLineOfSightToPlayer()
    {
        if(player == null) return false;
        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;
        direction = direction.normalized;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            distance,
            Constant.ObstacleLayerMaskValue
        );
        Debug.DrawRay(transform.position, direction * distance,
        hit.collider != null ? Color.red: Color.green);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag(Constant.PlayerTag))
            {
                return true;
            }
            return false;
        }
        return true;
    }
    public override void Attack()
    {
        _aimWeapon.HandleShooting();
    }
    protected override void Dead()
    {
        ChangeState(State.Death);
    }

    public override void ChangeState(State state)
    {
        switch (state)
        {
            case State.Idle:
                stateManager.ChangeState<IdleStateZombie>();
                break;
            case State.Attack:
                stateManager.ChangeState<AttackStateZombie>();
                break;
            case State.Chase:
                stateManager.ChangeState<ChaseStateZombie>();
                break;
            case State.Death:
                stateManager.ChangeState<DeathStateZombie>();
                break;
        }
    }
}
