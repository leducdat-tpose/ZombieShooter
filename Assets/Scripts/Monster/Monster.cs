using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    protected MonsterData monsterData;
    protected float currentHealth;
    protected Transform player;
    protected Rigidbody2D _rigid;
    protected enum MonsterState{
        Idle,
        Chase,
        Attack,
        Dead
    }
    protected MonsterState currentState;
    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
    }
    public virtual void Initialise(MonsterData monsterData, Transform playerTransform)
    {
        this.monsterData = monsterData;
        this.player = playerTransform;
        currentState = MonsterState.Idle;
    }
    private void Update() {
        switch (currentState)
        {
            case MonsterState.Idle:
                IdleBehaviour();
                break;
            case MonsterState.Chase:
                ChaseBehaviour();
                break;
            case MonsterState.Attack:
                AttackBehaviour();
                break;
            case MonsterState.Dead:
                DeadBehaviour();
                break;
        }
    }
    private void FixedUpdate() {
        if(currentState == MonsterState.Chase)
        {
            Move();
        }
    }

    protected bool ChangeState(MonsterState state)
    {
        if(currentState == state) return false;
        currentState = state;
        return true;
    }

    protected virtual void IdleBehaviour()
    {

    }
    protected virtual void ChaseBehaviour()
    {
        if(player == null)
        {
            ChangeState(MonsterState.Idle);
            return;
        }
        // RotateTowardTarget();
    }
    protected virtual void AttackBehaviour()
    {

    }
    protected virtual void DeadBehaviour()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth > 0) return;
        currentHealth = 0;
        Dead();
    }
    public virtual void Dead()
    {
        ChangeState(MonsterState.Dead);
    }
    protected virtual void RotateTowardTarget()
    {
        Vector2 targetDirection = player.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0,0,angle));
        transform.localRotation = Quaternion.Lerp(transform.localRotation, q, 0.01f);
    }
    protected virtual void Move()
    {
        Vector2 targetVelocity = (player.position - transform.position).normalized * monsterData.MoveSpeed;
        _rigid.velocity = targetVelocity;
    }
}
