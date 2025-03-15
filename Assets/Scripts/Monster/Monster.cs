using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Video;


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
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth > 0) return;
        currentHealth = 0;
        Dead();
    }
    public virtual bool HasLineOfSightToPlayer()
    {
        return true;
    }
    protected abstract void Dead();
    public abstract void Attack();
    public abstract void ChangeState(State state);
}
