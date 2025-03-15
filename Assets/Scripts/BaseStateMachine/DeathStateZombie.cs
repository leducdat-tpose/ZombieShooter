using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStateZombie : BaseState<MeleeZombie>
{
    public DeathStateZombie(MeleeZombie obj, StateManager<MeleeZombie> objectStateManager) : base(obj, objectStateManager)
    {
    }

    public override void EnterState()
    {
        owner.Rigid.velocity = Vector2.zero;
        Debug.Log("Dead state");
        owner.gameObject.SetActive(false);
    }

    public override void ExitState()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
    }
}
