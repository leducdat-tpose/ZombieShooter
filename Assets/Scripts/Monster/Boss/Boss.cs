using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    public enum AttackType{
        Melee,
        Ranged
    }
    public AttackType CurrentAttackType{get; private set;}
    private MonsterAimWeapon _aimWeapon;
    protected override void Start() {
        base.Start();
        CurrentAttackType = AttackType.Ranged;
        _aimWeapon = GetComponent<MonsterAimWeapon>();
        stateManager = new StateManager<Monster>();
        stateManager.AddState(new IdleStateZombie(this, stateManager));
        stateManager.AddState(new ChaseStateBoss(this, stateManager));
        stateManager.AddState(new AttackStateZombie(this, stateManager));
        stateManager.AddState(new DeathStateZombie(this, stateManager));
        stateManager.ChangeState<IdleStateZombie>();
    }
    private void Update() {
        stateManager.Update();
        Render();
    }
    private void FixedUpdate() {
        stateManager.FixedUpdate();
    }
    protected override void Dead()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        ChangeState(State.Death);
    }
    public override void Attack()
    {
        if(CurrentAttackType == AttackType.Ranged)
        {
            _aimWeapon.HandleShooting();
        }
        else if(CurrentAttackType == AttackType.Melee)
        {
            if(TargetTransform.TryGetComponent<Player>(out Player component))
            {
                component.TakeDamage(MonsterData.Damage);
            }
        }
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
                stateManager.ChangeState<ChaseStateBoss>();
                break;
            case State.Death:
                stateManager.ChangeState<DeathStateZombie>();
                break;
        }
    }
    public void SwitchAttackType()
    {
        if(CurrentAttackType == AttackType.Melee) CurrentAttackType = AttackType.Ranged;
        else CurrentAttackType = AttackType.Melee;
    }
    public override float GetAttackRange()
    {
        float attackRange = base.GetAttackRange();
        if(CurrentAttackType == AttackType.Ranged) attackRange *= 2f;
        return attackRange;
    }
}
