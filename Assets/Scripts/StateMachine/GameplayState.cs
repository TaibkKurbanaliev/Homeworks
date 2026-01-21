using System;

public class GameplayState : IState
{
    private GameStateMachine _stateMachine;
    private IGameplayHUD _hud;
    private IHealth _health;
    private Spawner _spawner;
    private GameController _controller;
    private IModifiersView _modifiersView;

    public GameplayState(GameStateMachine stateMachine, IGameplayHUD hud, IHealth health, 
                         Spawner spawner, GameController controller, IModifiersView modifiers)
    {
        _stateMachine = stateMachine;
        _hud = hud;
        _health = health;
        _spawner = spawner;
        _controller = controller;
        _modifiersView = modifiers;
    }

    public void Enter()
    {
        _hud.Show();
        _hud.PauseClicked += OnPauseClicked;
        _hud.InputTypeChanged += OnInputChanged;
        _health.Died += OnPlayerDied;
        _modifiersView.ModifierAdded += OnModifierAdd;
        _modifiersView.ModifierDeleted += OnModifierDeleted;
    }


    public void Exit()
    {
        _hud.Hide();
        _hud.PauseClicked -= OnPauseClicked;
        _hud.InputTypeChanged -= OnInputChanged;
        _health.Died -= OnPlayerDied;
        _modifiersView.ModifierAdded -= OnModifierAdd;
        _modifiersView.ModifierDeleted -= OnModifierDeleted;
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

    private void OnModifierDeleted(int index)
    {
        _controller.RemoveModifier(index);
    }

    private void OnModifierAdd(ModifierType type)
    {
        _controller.AddModifier(type);
    }
}
