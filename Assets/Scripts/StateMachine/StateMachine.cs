using System;
using System.Collections.Generic;
using System.Linq;

public class StateMachine : IStateSwitcher
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
        IState state = _states.FirstOrDefault(state => state is T);

        if (state == null)
            throw new ArgumentException($"StateMachine doesn't conatins {typeof(T)}");

        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void HandleInput() => _currentState.HandleInput();
    public void Update() => _currentState.Update();

    public void FixedUpdate() => _currentState.FixedUpdate();
}