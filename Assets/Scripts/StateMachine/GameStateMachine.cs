using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateMachine
{
    private List<IState> _states = new();
    private IState _currentState;

    public void AddState<T>(T state) where T : IState
    {
        if (_states.Any(st => st is T))
            throw new ArgumentException($"StateMachine is already exist {typeof(T)}");

        _states.Add(state);
    }

    public void SwitchState<T>() where T : IState
    {
        _currentState?.Exit();
        _currentState = _states.FirstOrDefault(state => state is T);
        _currentState.Enter();
    }
}
