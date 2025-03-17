using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateZombie : BaseState<Monster>
{
    public IdleStateZombie(Monster obj, StateManager<Monster> objectStateManager) : base(obj, objectStateManager)
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
            owner.ChangeState(Monster.State.Chase);
        }
    }

    public override void FixedUpdate()
    {
    }
}
