public class GameplayState : IState
{
    private GameStateMachine _stateMachine;
    private IGameplayHUD _hud;
    private IHealth _health;
    private Spawner _spawner;

    public GameplayState(GameStateMachine stateMachine, IGameplayHUD hud, IHealth health, Spawner spawner)
    {
        _stateMachine = stateMachine;
        _hud = hud;
        _health = health;
        _spawner = spawner;
    }

    public void Enter()
    {
        _hud.Show();
        _hud.PauseClicked += OnPauseClicked;
        _health.Died += OnPlayerDied;
        _spawner.gameObject.SetActive(true);
    }

    public void Exit()
    {
        _hud.Hide();
        _hud.PauseClicked -= OnPauseClicked;
        _health.Died -= OnPlayerDied;
        _spawner.gameObject.SetActive(false);
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
