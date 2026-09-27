using System;
using System.Collections.Generic;

public abstract class CharacterStateMachine<E> where E : Character
{
    public State<E> CurrentState { get; private set; }
    private Dictionary<Type, State<E>> _states = new Dictionary<Type, State<E>>();

    public void AddState(State<E> state)
    {
        _states.Add(state.GetType(), state);
    }

    public void ChangeState<T>() where T : State<E>
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
