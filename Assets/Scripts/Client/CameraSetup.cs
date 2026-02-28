using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    private EventBinding<PlayerConnectedToGame> _playerConnectedBinding;
    private EventBinding<DiedEvent> _playerDiedBinding;

    private Player _player;

    private void Awake()
    {
        _playerConnectedBinding = new EventBinding<PlayerConnectedToGame>(OnPlayerConnectedToGame);
        _playerDiedBinding = new EventBinding<DiedEvent>(OnPlayerDied);
        EventBus<PlayerConnectedToGame>.Register(_playerConnectedBinding);
        EventBus<DiedEvent>.Register(_playerDiedBinding);
    }

    private void OnDestroy()
    {
        _player.Health.Respawned -= OnPlayerRespawned;
        EventBus<PlayerConnectedToGame>.Deregister(_playerConnectedBinding);
        EventBus<DiedEvent>.Deregister(_playerDiedBinding);
    }

    private void OnPlayerConnectedToGame(PlayerConnectedToGame @event)
    {
        _player = @event.Player;
        _camera.Target.TrackingTarget = _player.CameraTarget;
        _player.Health.Respawned += OnPlayerRespawned;
    }

    private void OnPlayerRespawned()
    {
        _camera.Target.TrackingTarget = _player.CameraTarget;
    }

    private void OnPlayerDied(DiedEvent @event)
    {
        _camera.Target.TrackingTarget = _player.CameraGhostTarget;
    }
}
