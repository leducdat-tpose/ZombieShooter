using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class MeleeZombie : Monster
{
    
    protected override void Start() {
        base.Start();
        stateManager = new StateManager<Monster>();
        stateManager.AddState(new IdleStateZombie(this, stateManager));
        stateManager.AddState(new ChaseStateZombie(this, stateManager));
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
    public override void Attack()
    {
        if(TargetTransform.TryGetComponent<Player>(out Player component))
        {
            component.TakeDamage(MonsterData.Damage);
        }
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
