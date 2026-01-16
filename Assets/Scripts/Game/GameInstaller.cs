using UnityEngine;
using UnityEngine.SceneManagement;


[DefaultExecutionOrder(-2)]
public class GameInstaller : MonoBehaviour
{
    private const string k_Game = "Game";
    [SerializeField] private ServicesSOAP _services;
    [SerializeField] private Transform _target;

    private InputSystem_Actions _actions;
    
    private ILoggerService _loggerService;
    private IInputService _input;
    private ICollectibleService _collectible;
    
    public void Awake()
    {
        
        InitServices();

        if (!SceneManager.GetSceneByName(k_Game).isLoaded)
            SceneManager.LoadScene(k_Game, LoadSceneMode.Additive);
    }

    private void InitServices()
    {
        _actions = new InputSystem_Actions();
        _actions.Enable();

        _loggerService = new ConsoleLogger();
        _input = new DefaultInputService(_actions);
        _collectible = new CollectibleService();

        _services.GlobalServicesRegister(_loggerService, _input, _collectible, _actions);
    }

    private void OnDisable()
    {
        _actions.Disable();
    }
}
