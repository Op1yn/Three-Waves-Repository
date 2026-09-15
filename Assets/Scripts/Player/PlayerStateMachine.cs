using System;
using System.Collections.Generic;

public class PlayerStateMachine
{
    public State<Player> CurrentState { get; private set; }
    private Dictionary<Type, State<Player>> _states = new Dictionary<Type, State<Player>>();

    public void AddState(State<Player> state)
    {
        _states.Add(state.GetType(), state);
    }

    public void ChangeState<T>() where T : State<Player>
    {
        var type = typeof(T);

        if (CurrentState != null && CurrentState.GetType() == type)
            return;

        if (_states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void LateUpdate()
    {
        CurrentState?.LateUpdate();
    }
}