using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    private EventBinding<PlayerConnectedToGame> _playerConnectedBinding;
    private CinemachineCamera _camera;

    private void Awake()
    {
        _playerConnectedBinding = new EventBinding<PlayerConnectedToGame>(OnPlayerConnectedToGame);
        EventBus<PlayerConnectedToGame>.Register(_playerConnectedBinding);
    }

    private void OnDestroy()
    {
        EventBus<PlayerConnectedToGame>.Deregister(_playerConnectedBinding);
    }

    private void OnPlayerConnectedToGame(PlayerConnectedToGame @evemt)
    {

    }
}
