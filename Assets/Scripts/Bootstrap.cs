using System;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LeaderboardController _leaderboardController;

    private IInput _input;

    private EventBinding<PlayerConnectedToGame> _playerConnectedToGameBinding;

    private void Awake()
    {
        _input = new NewInputSystem();

        _leaderboardController.Init(_input);

        _playerConnectedToGameBinding = new EventBinding<PlayerConnectedToGame>(OnPlayerConnectedToGame);
        EventBus<PlayerConnectedToGame>.Register(_playerConnectedToGameBinding);
    }

    private void OnDestroy()
    {
        EventBus<PlayerConnectedToGame>.Deregister(_playerConnectedToGameBinding);
    }

    private void OnPlayerConnectedToGame(PlayerConnectedToGame game)
    {
        game.Player.SetInput(_input);
    }
}
