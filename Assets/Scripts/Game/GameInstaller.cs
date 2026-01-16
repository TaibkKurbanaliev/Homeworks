using UnityEngine;
using UnityEngine.SceneManagement;

public enum InputType
{
    Keyboard,
    AI,
    FakeInput,
}

[DefaultExecutionOrder(-2)]
public class GameInstaller : MonoBehaviour
{
    [SerializeField] private ServicesSOAP _services;
    //[SerializeField] private TMP_Dropdown _inputTypes;
    [SerializeField] private Transform _target;

    private InputSystem_Actions _actions;
    
    private ILoggerService _loggerService;
    private IInputService _input;
    
    public void Awake()
    {
        /*_inputTypes.AddOptions(Enum.GetNames(typeof(InputType)).ToList());
        _inputTypes.onValueChanged.AddListener(OnInputTypesValueChanged);*/
        InitServices();
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    private void InitServices()
    {
        _actions = new InputSystem_Actions();
        _actions.Enable();

        _loggerService = new ConsoleLogger();
        _input = new DefaultInputService(_actions);

        _services.GlobalServicesRegister(_loggerService, _input);
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    /*private void OnInputTypesValueChanged(int value)
    {
        switch((InputType)value)
        {
            case InputType.Keyboard:
                _input = new DefaultInputService(_actions);
                break;
            case InputType.AI:
                _input = new AIInputService(_target, Player.transform);
                break;
            case InputType.FakeInput:
                _input = new FakeInputService(Vector2.up);
                break;
            default:
                throw new NotImplementedException();
        }

        Player.Construct(_input, _movement, _health, _loggerService);

        _loggerService.Log(_inputTypes.options[value].text);
    }*/
}
