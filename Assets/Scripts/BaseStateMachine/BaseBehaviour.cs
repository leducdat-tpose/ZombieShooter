using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType{
    None,
    Idle,
    Attack,
    Walk,
    Death
}

public abstract class BaseBehaviour<T> : MonoBehaviour
{
    #region Animator
    protected Animator animator;
    public int CurrentAnimation{get; protected set;}
    #endregion
    protected SpriteRenderer spriteRenderer;
    protected T Object;
    #region State
    public StateManager<T> StateManager;
    public BaseState<T> IdleState;
    public BaseState<T> WalkState;
    public BaseState<T> AttackState;
    public BaseState<T> DeathState;
    #endregion
    public virtual void Start()
    {
        Object = this.gameObject.GetComponent<T>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        animator = this.gameObject.GetComponent<Animator>();
        StateManager = new StateManager<T>();
    }

    public abstract void Update();

    public abstract void FixedUpdate();
    public abstract void Render();

    public void ChangeState(StateType type)
    {
        switch (type)
        {
            case StateType.Idle:
                StateManager.ChangeState(IdleState);
                break;
            case StateType.Walk:
                StateManager.ChangeState(WalkState);
                break;
            case StateType.Attack:
                StateManager.ChangeState(AttackState);
                break;
            case StateType.Death:
                StateManager.ChangeState(DeathState);
                break;
            default:
                return;
        }
    }

    public BaseState<T> GetCurrentState()
    {
        return StateManager.CurrentState;
    }

    public BaseState<T> GetState(StateType type)
    {
        return type switch
        {
            StateType.Idle => IdleState,
            StateType.Walk => WalkState,
            StateType.Attack => AttackState,
            StateType.Death => DeathState,
            _ => IdleState,
        };
    }
    public virtual void CauseDamage()
    {
        if(StateManager.CurrentState != AttackState) return;
    }

    public virtual void ReadyToAttack(){}
    public virtual void ReadyProjectile(){}
    public virtual void StartAttacking(){}
    public virtual void StopAttacking()
    {
        StateManager.ChangeState(IdleState);
    }

    public virtual bool GetIsDead() => StateManager.CurrentState == DeathState;
}
