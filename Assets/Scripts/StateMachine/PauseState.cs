using System;
using UnityEngine;

public class PauseState : IState
{
    private GameStateMachine _stateMachine;
    private IPauseView _view;

    public PauseState(GameStateMachine stateMachine, IPauseView view)
    {
        _stateMachine = stateMachine;
        _view = view;
    }

    public void Enter()
    {
        Time.timeScale = 0f;
        _view.Show();
        _view.MenuClicked += OnMenuClicked;
        _view.ResumeClicked += OnResumeClicked;
    }

    public void Exit()
    {
        _view.Hide();
        Time.timeScale = 1f;
    }

    private void OnResumeClicked()
    {
        _stateMachine.SwitchState<GameplayState>();
    }

    private void OnMenuClicked()
    {
        _stateMachine.SwitchState<MainMenuState>();
    }
}
