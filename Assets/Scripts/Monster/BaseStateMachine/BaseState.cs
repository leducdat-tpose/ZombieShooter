using UnityEngine;

public abstract class BaseState<T>
{
    protected readonly T owner;
    protected StateManager<T> stateManager;

    protected BaseState(T obj, StateManager<T> objectStateManager)
    {
        owner = obj;
        stateManager = objectStateManager;
    }
    
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void Update();
    public abstract void FixedUpdate();
    public virtual void AnimationTriggerEvent() { }
    public virtual void GetNextState() { }
    public virtual void OnTriggerEnter(Collider2D collision) { }
    public virtual void OnTriggerStay(Collider2D collision) { }
    public virtual void OnTriggerExit(Collider2D collision) { }
}
