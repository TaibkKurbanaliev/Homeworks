using UnityEngine;

public class GameOverState : IState
{
    private GameStateMachine _stateMachine;
    private IGameOverView _view;

    public GameOverState(GameStateMachine stateMachine, IGameOverView view)
    {
        _stateMachine = stateMachine;
        _view = view;
    }

    public void Enter()
    {
        Time.timeScale = 0f;
        _view.Show();
        _view.MenuClicked += OnMenuClicked;
        _view.RestartClicked += OnRestartClicked;
    }

    public void Exit()
    {
        _view.Hide();
        Time.timeScale = 1f;
    }

    private void OnRestartClicked()
    {
        _stateMachine.SwitchState<GameplayState>();
    }

    private void OnMenuClicked()
    {
        _stateMachine.SwitchState<MainMenuState>();
    }
}
