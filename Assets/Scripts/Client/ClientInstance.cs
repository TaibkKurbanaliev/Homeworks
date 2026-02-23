using Mirror;
using System;
using UnityEngine;

public class ClientInstance : NetworkBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerLobbyView _playerLobbyView;

    public Player CurrentPlayer { get; private set; }

    [Server]
    public void NetworkCreatePlayer(Transform _spawnPoint)
    {
        var player = Instantiate(_player, _spawnPoint.position, _spawnPoint.rotation);
        NetworkServer.Spawn(player.gameObject, connectionToClient);
        player.Init(GetComponent<InstanceInfo>());
        CurrentPlayer = player;
    }
}
