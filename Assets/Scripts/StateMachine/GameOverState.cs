using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : IState
{
    private GameStateMachine _stateMachine;
    private IGameOverView _view;
    private GameController _controller;

    public GameOverState(GameStateMachine stateMachine, IGameOverView view)
    {
        _stateMachine = stateMachine;
        _view = view;
    }

    public GameOverState(GameStateMachine stateMachine, IGameOverView view, GameController controller) : this(stateMachine, view)
    {
        _controller = controller;
    }

    public void Enter()
    {
        _view.Show();
        _view.MenuClicked += OnMenuClicked;
        _view.RestartClicked += OnRestartClicked;
    }

    public void Exit()
    {
        _view.MenuClicked -= OnMenuClicked;
        _view.RestartClicked -= OnRestartClicked;
        _view.Hide();
    }

    private void OnRestartClicked()
    {
        _stateMachine.SwitchState<GameplayState>();
        _controller.StartLevel();
    }

    private void OnMenuClicked()
    {
        _controller.LoadMenu();
    }
}
