using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : IStateSwitcher, IDisposable
{
    private List<IState> _states = new();
    public IState CurrentState { get; private set; }

    public void AddState<T>(T state) where T : IState
    {
        if (_states.Any(st => st is T))
            throw new ArgumentException($"StateMachine is already exist {typeof(T)}");

        _states.Add(state);
    }

    public void Dispose()
    {
        CurrentState?.Exit();
    }

    public void SwitchState<T>() where T : IState
    {
        CurrentState?.Exit();
        CurrentState = _states.FirstOrDefault(st => st is T);
        CurrentState.Enter();
    }

    public void Update() => CurrentState?.Update();
}
