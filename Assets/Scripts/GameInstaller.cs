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
