using Mirror;
using System;
using System.Collections.Generic;
using UniExtension;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class BootstrapServer : NetworkBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private List<Transform> _spawnPositions;

    public override void OnStartServer()
    {
        NetworkManagerExt.singleton.ServerPlayerConnected += OnServerPlayerConnected;
        NetworkManagerExt.singleton.ServerPlayerDisconnected += OnServerPlayerDisconnected;
    }

    public override void OnStopServer()
    {
        NetworkManagerExt.singleton.ServerPlayerConnected -= OnServerPlayerConnected;
        NetworkManagerExt.singleton.ServerPlayerDisconnected -= OnServerPlayerDisconnected;
    }

    private void OnServerPlayerDisconnected(NetworkConnection connection)
    {
    }

    private void OnServerPlayerConnected(NetworkConnection connection)
    {
        var client = NetworkManagerExt.LocalPlayers[connection].GetComponent<ClientInstance>();
        client.NetworkCreatePlayer(_spawnPositions.GetRandomElement());
    }
}