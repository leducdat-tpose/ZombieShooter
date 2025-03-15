using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class MeleeZombie : Monster
{
    private StateManager<MeleeZombie> _stateManager;
    protected override void Start() {
        base.Start();
        _stateManager = new StateManager<MeleeZombie>();
        _stateManager.AddState(new IdleStateZombie(this, _stateManager));
        _stateManager.AddState(new ChaseStateZombie(this, _stateManager));
        _stateManager.AddState(new AttackStateZombie(this, _stateManager));
        _stateManager.AddState(new DeathStateZombie(this, _stateManager));
        _stateManager.ChangeState<IdleStateZombie>();
    }
    private void Update() {
        _stateManager.Update();
    }
    private void FixedUpdate() {
        _stateManager.FixedUpdate();
    }
    public override void Dead()
    {
        _stateManager.ChangeState<DeathStateZombie>();
    }
}
