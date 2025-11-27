using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Player _player;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private List<EnemyConfig> _configs;

    private PlayerInputActions _input;
    private GameStateService _stateService;
    private SpawnService _spawnService;

    private void Awake()
    {
        _input = new PlayerInputActions();
        _input.Enable();

        _stateService = new GameStateService(_gameConfig.TargetScore);
        _uIManager.Init(_stateService);
        _spawnService = new(_gameConfig.SpawnFrequencyInMiliseconds, _enemyPrefab, _spawnPoints, _player, _configs);
        _player.Init(_input, _gameConfig.PlayerSpeed, 3);
        _ = _spawnService.SpawnEnemyAsync(destroyCancellationToken);
    }

    private void OnDisable()
    {
        _stateService.Dispose();
        _spawnService.Dispose();
        _input.Disable();
    }

    private void Update()
    {
        _stateService.Update();
    }
}
