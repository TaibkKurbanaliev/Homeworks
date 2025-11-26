using System;
using System.IO;
using UnityEngine;

public class GameStateService : IDisposable
{
    private StateMachine _stateMachine;
    private bool _isPaused = false;

    public readonly int WinScore;
    public int CurrentScore { get; private set; }

    public GameStateService(int winScore)
    {
        WinScore = winScore;
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new Init(_stateMachine));
        _stateMachine.AddState(new Playing(_stateMachine, this));
        _stateMachine.AddState(new Pause(_stateMachine));
        _stateMachine.AddState(new Win());
        _stateMachine.SwitchState<Init>();
    }

    public void SetPauseState()
    {
        if (_stateMachine.CurrentState is Win)
            return;

        _stateMachine.SwitchState<Pause>();
    }

    public void Dispose()
    {
        _stateMachine.Dispose();
    }

    public void AddScore(int score)
    {
        if (score <= 0)
            throw new InvalidDataException(nameof(score));

        CurrentScore += score;
    }

    public void Update() => _stateMachine.Update();
}
