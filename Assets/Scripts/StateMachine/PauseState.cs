using System;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : IState
{
    private GameStateMachine _stateMachine;
    private IPauseView _view;
    private List<IPauseEntity> _pauseEntities;
    private GameController _controller;

    public PauseState(GameStateMachine stateMachine, IPauseView view, List<IPauseEntity> pauseEntities, GameController controller)
    {
        _stateMachine = stateMachine;
        _view = view;
        _pauseEntities = pauseEntities;
        _controller = controller;
    }

    public void Enter()
    {
        _view.Show();
        _view.MenuClicked += OnMenuClicked;
        _view.ResumeClicked += OnResumeClicked;
        
        foreach (var entity in _pauseEntities)
        {
            entity.Pause(true);
        }
    }

    public void Exit()
    {
        _view.Hide();

        foreach (var entity in _pauseEntities)
        {
            entity.Pause(false);
        }
    }

    private void OnResumeClicked()
    {
        _stateMachine.SwitchState<GameplayState>();
    }

    private void OnMenuClicked()
    {
        _stateMachine.SwitchState<MainMenuState>();
        _controller.LoadMenu();
    }
}
