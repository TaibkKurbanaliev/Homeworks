using System;
using System.Linq;
using TMPro;
using UnityEngine;

public enum InputType
{
    Keyboard,
    AI,
    FakeInput,
}

[DefaultExecutionOrder(-1)]
public class GameInstaller : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Dropdown _inputTypes;
    [SerializeField] private Transform _target;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private MainMenuView _menuView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private GameplayHUD _hudView;

    private GameStateMachine _stateMachine;

    private InputSystem_Actions _actions;
    private ILoggerService _loggerService;
    private IInputService _input;
    private IMovementService _movement;
    private IHealth _health;

    public void Awake()
    {
        _inputTypes.AddOptions(Enum.GetNames(typeof(InputType)).ToList());
        _inputTypes.onValueChanged.AddListener(OnInputTypesValueChanged);

        _actions = new InputSystem_Actions();
        _actions.Enable();

        _loggerService = new ConsoleLogger();
        _input = new DefaultInputService(_actions);
        _movement = new RigidbodyMovement(_player.GetComponent<Rigidbody>());
        _health = new Health(100f, _loggerService);

        _player.Construct(_input, _movement, _health, _loggerService);

        _loggerService.Log("StartGame");

        _hudView.Construct(_health);

        _stateMachine = new GameStateMachine();
        _stateMachine.AddState(new MainMenuState(_stateMachine, _menuView));
        _stateMachine.AddState(new GameplayState(_stateMachine, _hudView, _health));
        _stateMachine.AddState(new PauseState(_stateMachine, _pauseView));
        _stateMachine.AddState(new GameOverState(_stateMachine, _gameOverView));
        _stateMachine.SwitchState<MainMenuState>();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    private void OnInputTypesValueChanged(int value)
    {
        switch((InputType)value)
        {
            case InputType.Keyboard:
                _input = new DefaultInputService(_actions);
                break;
            case InputType.AI:
                _input = new AIInputService(_target, _player.transform);
                break;
            case InputType.FakeInput:
                _input = new FakeInputService(Vector2.up);
                break;
            default:
                throw new NotImplementedException();
        }

        _player.Construct(_input, _movement, _health, _loggerService);

        _loggerService.Log(_inputTypes.options[value].text);
    }
}
