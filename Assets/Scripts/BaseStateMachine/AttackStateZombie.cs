using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateZombie : BaseState<Monster>
{
    private float _lastAttackTime;
    private bool _canAttack;
    private Coroutine _coroutine;
    public AttackStateZombie(Monster obj, StateManager<Monster> objectStateManager) : base(obj, objectStateManager)
    {
    }
    public override void EnterState()
    {
        owner.Rigid.velocity = Vector2.zero;
        _lastAttackTime = 0;
        _canAttack = true;
    }

    public override void ExitState()
    {
        MonoBehaviour mb = owner as MonoBehaviour;
        if(_coroutine != null) mb.StopCoroutine(_coroutine);
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        float distance = Vector2.Distance(owner.TargetTransform.position, owner.transform.position);
        if(distance > owner.GetAttackRange() || !owner.HasLineOfSightToPlayer())
        {
            owner.ChangeState(Monster.State.Chase);
            return;
        }
        if(_canAttack && Time.time >= _lastAttackTime + owner.MonsterData.AttackCoolDown)
        {
            owner.Attack();
            _coroutine = owner.StartCoroutine(CanAttackCoroutine());
        }
    }
    private IEnumerator CanAttackCoroutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(0.5f);
        _canAttack = true;
    }
    
}
