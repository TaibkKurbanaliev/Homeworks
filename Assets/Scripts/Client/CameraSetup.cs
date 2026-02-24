using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    private EventBinding<PlayerConnectedToGame> _playerConnectedBinding;

    private void Awake()
    {
        _playerConnectedBinding = new EventBinding<PlayerConnectedToGame>(OnPlayerConnectedToGame);
        EventBus<PlayerConnectedToGame>.Register(_playerConnectedBinding);
    }

    private void OnDestroy()
    {
        EventBus<PlayerConnectedToGame>.Deregister(_playerConnectedBinding);
    }

    private void OnPlayerConnectedToGame(PlayerConnectedToGame @event)
    {
        _camera.Target.TrackingTarget = @event.Player.CameraTarget;
    }
}
