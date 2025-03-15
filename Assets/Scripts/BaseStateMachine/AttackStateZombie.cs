using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateZombie : BaseState<MeleeZombie>
{
    private float _lastAttackTime;
    private bool _canAttack;
    private Coroutine _coroutine;
    public AttackStateZombie(MeleeZombie obj, StateManager<MeleeZombie> objectStateManager) : base(obj, objectStateManager)
    {
    }
    public override void EnterState()
    {
        owner.Rigid.velocity = Vector2.zero;
        _canAttack = true;
        _lastAttackTime = 0;
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
        if(distance > owner.MonsterData.AttackRange)
        {
            stateManager.ChangeState<ChaseStateZombie>();
            return;
        }
        if(_canAttack && Time.time >= _lastAttackTime + owner.MonsterData.AttackCoolDown)
        {
            Attack();
            _coroutine = owner.StartCoroutine(CanAttackCoroutine());
        }
    }
    private IEnumerator CanAttackCoroutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log($"CanAttackCoroutine");
        _canAttack = true;
    }
    private void Attack()
    {
        _lastAttackTime = Time.time;
        if(owner.TargetTransform.TryGetComponent<Player>(out Player component))
        {
            component.TakeDamage(owner.MonsterData.Damage);
        }
    }
}
