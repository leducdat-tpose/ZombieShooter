using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Runtime.InteropServices.WindowsRuntime;
public class ChaseStateZombie : BaseState<Monster>
{
    private Coroutine _coroutine;
    private Path _path;
    private int _currentWaypoint = 0;
    private int _nextWayPointDistance = 1;
    public ChaseStateZombie(Monster obj, StateManager<Monster> objectStateManager) : base(obj, objectStateManager)
    {
    }

    public override void EnterState()
    {
        MonoBehaviour mb = owner as MonoBehaviour;
        if(mb != null)
        {
            _coroutine = mb.StartCoroutine(UpdatePathCoroutine());
        }
    }

    public override void ExitState()
    {
        MonoBehaviour mb = owner as MonoBehaviour;
        if(mb != null)
        {
            mb.StopCoroutine(_coroutine);
        }
        owner.Seeker.CancelCurrentPathRequest();
        _path = null;
    }

    public override void Update()
    {
        float distance = Vector2.Distance(owner.TargetTransform.position, owner.transform.position);
        bool canSeePlayer = owner.HasLineOfSightToPlayer();
        if(distance <= owner.MonsterData.AttackRange && canSeePlayer)
        {
            owner.ChangeState(Monster.State.Attack);
            return;
        }
        if(distance > owner.MonsterData.DetectionRange * 1.5f || (!canSeePlayer && distance > owner.MonsterData.AttackRange * 1.5f))
        {
            owner.ChangeState(Monster.State.Idle);
            return;
        }
    }

    public override void FixedUpdate()
    {
        Chasing();
    }
    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }
    private IEnumerator UpdatePathCoroutine()
    {
        while(true)
        {
            UpdatePath();
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void UpdatePath()
    {
        if(owner.Seeker.IsDone())
        {
            owner.Seeker.StartPath(owner.Rigid.position, owner.TargetTransform.position, OnPathComplete);
        }
    }
    private void Chasing()
    {
        if(_path == null) return;
        if(_currentWaypoint >= _path.vectorPath.Count)
        {
            return;
        }
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - owner.Rigid.position).normalized;
        Vector2 force = direction * owner.MonsterData.MoveSpeed*100 * Time.fixedDeltaTime;
        owner.Rigid.AddForce(force);
        float distance = Vector2.Distance(owner.Rigid.position, _path.vectorPath[_currentWaypoint]);
        if(distance < _nextWayPointDistance) _currentWaypoint++;
    }
}
