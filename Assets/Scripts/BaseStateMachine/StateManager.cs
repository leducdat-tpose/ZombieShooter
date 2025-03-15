using System.Collections;
using System.Collections.Generic;

public class StateManager<T>
{
    private BaseState<T> _currentState;
    private Dictionary<System.Type, BaseState<T>> states = new Dictionary<System.Type, BaseState<T>>();
    public void AddState(BaseState<T> state)
    {
        states[state.GetType()] = state;
    }

    public void ChangeState<TState>() where TState : BaseState<T>
    {
        if(_currentState != null)
        {
            _currentState.ExitState();
        }
        _currentState = states[typeof(TState)];
        _currentState.EnterState();
    }
    public void Update()
    {
        _currentState?.Update();
    }
    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }

    public System.Type GetCurrentStateType() => _currentState?.GetType();
}
