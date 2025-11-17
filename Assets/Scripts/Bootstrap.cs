using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Bootstrap : MonoBehaviour
{
    [Header("Initializable Objects")]
    [SerializeField] private Player _player;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Analytics _analytics;
    [Header("EnemyManager Settings")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _spawnPoint;
    [Header("Weater Settings")]
    [SerializeField] private GameObject _dayBackground;
    [SerializeField] private GameObject _nightBackground;

    private PlayerInputActions _actions;
    private EventManager _eventManager;
    private EnemyManager _enemyManager;
    private WeatherChanger _weatherChanger;

    private void Awake()
    {
        _eventManager = new EventManager();
        _weatherChanger = new WeatherChanger(_eventManager, _dayBackground, _nightBackground);
        _actions = new PlayerInputActions();
        _actions.Enable();

        _analytics.Init(_eventManager);
        _player.Init(_actions, _eventManager);
        _uiManager.Init(_eventManager);
        _enemyManager = new(_enemy, _spawnPoint, _eventManager, _player);
    }

    private void OnDisable()
    {
        _weatherChanger.Dispose();
        _enemyManager.Dispose();
        _actions.Disable();
    }
}
