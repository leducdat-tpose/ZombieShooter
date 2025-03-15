using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public abstract class Monster : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected MonsterData monsterData;
    public MonsterData MonsterData => monsterData; 
    protected float currentHealth;
    protected Transform player;
    public Transform TargetTransform => player;
    protected Rigidbody2D rigid;
    public Rigidbody2D Rigid => rigid;
    public Seeker Seeker{get; private set;}
    protected float currentAttackCoolDown =0f;
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        Seeker = GetComponent<Seeker>();
    }
    public virtual void Initialise(MonsterData monsterData, Transform playerTransform)
    {
        this.monsterData = monsterData;
        this.player = playerTransform;
    }

    protected virtual void Start() {
        player = GameObject.FindGameObjectWithTag(Constant.PlayerTag).transform;
        currentHealth = monsterData.Health;
    }

    protected virtual void IdleBehaviour()
    {

    }
    protected virtual void RoamBehaviour()
    {

    }
    protected virtual void ChaseBehaviour()
    {
    }
    protected virtual void AttackBehaviour()
    {

    }
    protected virtual void DeadBehaviour()
    {
        Destroy(this.gameObject);
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
    }
    public virtual void Chasing()
    {
        Vector2 targetVelocity = (player.position - transform.position).normalized * monsterData.MoveSpeed;
        rigid.MovePosition(rigid.position + targetVelocity*Time.fixedDeltaTime);
    }
    protected virtual void Attack()
    {

    }
}
