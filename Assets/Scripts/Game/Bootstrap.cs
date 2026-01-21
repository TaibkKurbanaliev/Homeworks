using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ServicesSOAP _services;
    [SerializeField] private GameOverView _gameOverView;
    [Header("View")]
    [SerializeField] private MainMenuView _menuView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private GameplayHUD _hudView;
    [SerializeField] private ModifiersView _modifiersView;
    [Header("Scene Objects")]
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private List<Trap> _traps;
    [Header("Configs")]
    [SerializeField] private CollectibleConfig _coinConfig;
    [SerializeField] private ModifierConfig _modifierConfig;

    private List<IPauseEntity> _pauseEntities = new();
    private GameStateMachine _stateMachine;
    private GameController _controller;

    private void Awake()
    {
        _player = Instantiate(_player);
        _player.gameObject.SetActive(false);

        _pauseEntities.Add(_player);
        _pauseEntities.Add(_spawner);
        
        foreach(var trap in _traps)
        {
            _pauseEntities.Add(trap);
        }

        var movement = new RigidbodyMovement(_player.GetComponent<Rigidbody>());
        var health = new Health(100f, _services.LoggerService);
        _services.PlayerServicesRegister(movement, health);

        _hudView.Construct(_services.Health, _services.Collectible);
        _controller = new GameController(_services, _spawner, _coinConfig, _player, _services.Collectible, _modifierConfig, _modifiersView);

        SetupStateMachine();
        _stateMachine.SwitchState<MainMenuState>();
    }

    private void SetupStateMachine()
    {
        _stateMachine = new GameStateMachine();
        _stateMachine.AddState(new MainMenuState(_stateMachine, _menuView, _controller));
        _stateMachine.AddState(new GameplayState(_stateMachine, _hudView, _services.Health, _spawner, 
                                                 _controller, _modifiersView));
        _stateMachine.AddState(new PauseState(_stateMachine, _pauseView, _pauseEntities, _controller));
        _stateMachine.AddState(new GameOverState(_stateMachine, _gameOverView, _controller));
    }
}
