using System;

public class GameplayState : IState
{
    private GameStateMachine _stateMachine;
    private IGameplayHUD _hud;
    private IHealth _health;
    private Spawner _spawner;
    private GameController _controller;

    public GameplayState(GameStateMachine stateMachine, IGameplayHUD hud, IHealth health, Spawner spawner, GameController controller)
    {
        _stateMachine = stateMachine;
        _hud = hud;
        _health = health;
        _spawner = spawner;
        _controller = controller;
    }

    public void Enter()
    {
        _hud.Show();
        _hud.PauseClicked += OnPauseClicked;
        _hud.InputTypeChanged += OnInputChanged;
        _health.Died += OnPlayerDied;
    }

    public void Exit()
    {
        _hud.Hide();
        _hud.PauseClicked -= OnPauseClicked;
        _hud.InputTypeChanged -= OnInputChanged;
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

    private void OnInputChanged(InputType type)
    {
        _controller.ChangeInputType(type);
    }
}
