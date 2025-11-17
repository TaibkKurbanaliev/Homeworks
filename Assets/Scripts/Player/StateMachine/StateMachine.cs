using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : IStateSwitcher
{
    private List<IState> _states = new();
    private IState _currentState;

    public void SwitchState<T>() where T : IState
    {
        _currentState?.Exit();
        _currentState = _states.FirstOrDefault(state => state is T);
        _currentState.Enter();
    }

    public void AddState(IState state)
    {
        _states.Add(state);
    }

    public void Update() => _currentState.Update();
    public void FixedUpdate() => _currentState.FixedUpdate();
}
