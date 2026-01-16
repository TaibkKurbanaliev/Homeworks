using System;
using UnityEngine;

public class MainMenuState : IState
{
    private GameStateMachine _stateMachine;
    private IMainMenuView _mainMenuView;
    private GameController _controller;

    public MainMenuState(GameStateMachine stateMachine, IMainMenuView mainMenuView, GameController controller)
    {
        _stateMachine = stateMachine;
        _mainMenuView = mainMenuView;
        _controller = controller;
    }

    public void Enter()
    {
        _mainMenuView.Show();
        _mainMenuView.StartClicked += OnStartClicked;
        _mainMenuView.ExitClicked += OnExitClicked;
    }

    public void Exit()
    {
        _mainMenuView.Hide();
        _mainMenuView.StartClicked -= OnStartClicked;
        _mainMenuView.ExitClicked -= OnExitClicked;
    }

    private void OnExitClicked()
    {
        Application.Quit();
    }

    private void OnStartClicked()
    {
        _stateMachine.SwitchState<GameplayState>();
        _controller.StartLevel();
    }
}
