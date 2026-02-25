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
        player.Init(GetComponent<InstanceInfo>());
        NetworkServer.Spawn(player.gameObject, connectionToClient);
        CurrentPlayer = player;
    }
}
