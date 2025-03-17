using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Video;
using System;


public abstract class Monster : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField]
    protected MonsterData monsterData;
    public MonsterData MonsterData => monsterData; 
    protected float currentHealth;
    protected Transform player;
    public Action OnMonsterDeath;
    public Transform TargetTransform => player;
    protected CircleCollider2D circleCollider;
    protected Rigidbody2D rigid;
    public Rigidbody2D Rigid => rigid;
    protected SpriteRenderer spriteRender;
    protected Animator animator;
    protected int currentAnimation;
    protected StateManager<Monster> stateManager;
    public enum State{
        Idle,
        Chase,
        Attack,
        Death
    }
    public Seeker Seeker{get; private set;}
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        Seeker = GetComponent<Seeker>();
        spriteRender = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }
    public virtual void Initialise(MonsterData monsterData, Transform playerTransform)
    {
        this.monsterData = monsterData;
        this.player = playerTransform;
    }

    protected virtual void Start() {
        player = GameObject.FindGameObjectWithTag(Constant.PlayerTag).transform;
        currentHealth = monsterData.MaxHealth;
    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth > 0) return;
        currentHealth = 0;
        Dead();
    }
    public float GetCurrentHealth() => currentHealth;
    public virtual void Healing(float healthAmount)
    {
        currentHealth += healthAmount;
        if(currentHealth > monsterData.MaxHealth) currentHealth = monsterData.MaxHealth;
    }
    public virtual bool HasLineOfSightToPlayer()
    {
        return true;
    }
    protected virtual void Dead()
    {
        circleCollider.enabled = false;
        OnMonsterDeath?.Invoke();
        ChangeState(State.Death);
    }
    public abstract void Attack();
    public abstract void ChangeState(State state);
    public virtual void Render()
    {
        spriteRender.flipX = rigid.velocity.x < 0;
        int newAnimation = Constant.IdleAnimation;
        if(rigid.velocity != Vector2.zero) newAnimation = Constant.WalkAnimation;
        if(stateManager.GetCurrentStateType() == typeof(DeathStateZombie)) newAnimation = Constant.DeathAnimation;
        if(currentAnimation == newAnimation) return;
        currentAnimation = newAnimation;
        animator.CrossFade(currentAnimation, 0.1f, 0);
    }
    public virtual float GetAttackRange() => monsterData.AttackRange;

    public void OnObjectSpawn()
    {
        if(stateManager == null) return;
        currentHealth = monsterData.MaxHealth;
        ChangeState(State.Idle);
        circleCollider.enabled = true;
    }
}
