using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : IStateSwitcher
{
    private List<IState> _states = new();
    public IState CurrentState { get; private set; }

    public void AddState<T>(T state) where T : IState
    {
        if (_states.Any(st => st is T))
            throw new ArgumentException($"StateMachine is already exist {typeof(T)}");

        _states.Add(state);
    }

    public void SwitchState<T>() where T : IState
    {
        CurrentState?.Exit();
        CurrentState = _states.FirstOrDefault(state => state is T);
        CurrentState.Enter();
    }

    public void Update() => CurrentState.Update();
    public void FixedUpdate() => CurrentState.FixedUpdate();
}
