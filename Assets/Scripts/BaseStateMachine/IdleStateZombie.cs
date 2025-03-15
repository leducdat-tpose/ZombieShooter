using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateZombie : BaseState<MeleeZombie>
{
    public IdleStateZombie(MeleeZombie obj, StateManager<MeleeZombie> objectStateManager) : base(obj, objectStateManager)
    {
    }

    public override void EnterState()
    {
        owner.Rigid.velocity = Vector2.zero;
    }
    public override void ExitState()
    {
    }

    public override void Update()
    {
        float distance = Vector2.Distance(owner.TargetTransform.position, owner.transform.position);
        if(distance <= owner.MonsterData.DetectionRange)
        {
            stateManager.ChangeState<ChaseStateZombie>();
        }
    }

    public override void FixedUpdate()
    {
    }
}
