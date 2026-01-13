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

    private InputSystem_Actions _actions;
    private ILoggerService _loggerService;
    private IInputService _input;
    private IMovementService _movement;

    public void Awake()
    {
        _inputTypes.AddOptions(Enum.GetNames(typeof(InputType)).ToList());
        _inputTypes.onValueChanged.AddListener(OnInputTypesValueChanged);

        _actions = new InputSystem_Actions();
        _actions.Enable();

        _loggerService = new ConsoleLogger();
        _input = new DefaultInputService(_actions);
        _movement = new RigidbodyMovement(_player.GetComponent<Rigidbody>());
        IHealth health = new Health(100f, _loggerService);

        _player.Construct(_input, _movement, health);
        _loggerService.Log("StartGame");
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    private void OnInputTypesValueChanged(int value)
    {
        _loggerService.Log(_inputTypes.options[value].text);
    }
}
