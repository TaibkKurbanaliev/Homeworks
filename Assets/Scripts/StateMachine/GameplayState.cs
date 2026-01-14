using System;
using UnityEngine;

public class GameplayState : IState
{
    private GameStateMachine _stateMachine;
    private IGameplayHUD _hud;
    private IHealth _health;

    public GameplayState(GameStateMachine stateMachine, IGameplayHUD hud, IHealth health)
    {
        _stateMachine = stateMachine;
        _hud = hud;
        _health = health;
    }

    public void Enter()
    {
        _hud.Show();
        _hud.PauseClicked += OnPauseClicked;
        _health.Died += OnPlayerDied;
    }

    public void Exit()
    {
        _hud.Hide();
        _hud.PauseClicked -= OnPauseClicked;
        _health.Died -= OnPlayerDied;
    }

    private void OnPauseClicked()
    {
        _stateMachine.SwitchState<PauseState>();
    }

    private void OnPlayerDied()
    {
        _stateMachine.SwitchState<GameOverState>();
    }
}
