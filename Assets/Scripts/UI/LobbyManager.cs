using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    [SerializeField] private PlayerLobbyView _viewPrefab;
    [SerializeField] private Transform _container;

    private List<PlayerLobbyView> _playersViews = new List<PlayerLobbyView>();

    private void Awake()
    {
        NetworkManagerExt.singleton.OnServerPlayerConnected += OnServerPlayerConnected;
        NetworkManagerExt.singleton.OnServerPlayerDisconnected += OnServerPlayerDisconnected;
    }

    private void OnServerPlayerConnected()
    {
        RpcAddPlayer();
    }

    private void OnServerPlayerDisconnected()
    {
    }

    [ClientRpc]
    private void RpcAddPlayer()
    {
        var playerView = Instantiate(_viewPrefab, _container);
        _playersViews.Add(playerView);
    }
}
