using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ServicesSOAP _services;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private MainMenuView _menuView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private GameplayHUD _hudView;
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CollectibleConfig _coinConfig;

    private List<IPauseEntity> _pauseEntities = new();
    private GameStateMachine _stateMachine;
    private GameController _controller;

    private void Awake()
    {
        _player = Instantiate(_player);
        _player.gameObject.SetActive(false);

        _pauseEntities.Add(_player);
        _pauseEntities.Add(_spawner);


        var movement = new RigidbodyMovement(_player.GetComponent<Rigidbody>());
        var health = new Health(100f, _services.LoggerService);
        _services.PlayerServicesRegister(movement, health);

        _hudView.Construct(_services.Health, _services.Collectible);
        _controller = new GameController(_services, _spawner, _coinConfig, _player, _services.Collectible);

        SetupStateMachine();
        _stateMachine.SwitchState<MainMenuState>();
    }

    private void SetupStateMachine()
    {
        _stateMachine = new GameStateMachine();
        _stateMachine.AddState(new MainMenuState(_stateMachine, _menuView, _controller));
        _stateMachine.AddState(new GameplayState(_stateMachine, _hudView, _services.Health, _spawner));
        _stateMachine.AddState(new PauseState(_stateMachine, _pauseView, _pauseEntities, _controller));
        _stateMachine.AddState(new GameOverState(_stateMachine, _gameOverView, _controller));
    }
}
