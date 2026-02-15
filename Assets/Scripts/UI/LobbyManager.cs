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
        NetworkManagerExt.singleton.ServerPlayerConnected += OnServerPlayerConnected; ;
        NetworkManagerExt.singleton.ServerPlayerDisconnected += OnServerPlayerDisconnected; ;
    }

    private void OnServerPlayerDisconnected()
    {
        throw new NotImplementedException();
    }

    private void OnServerPlayerConnected(NetworkConnection conn)
    {
        if (NetworkManagerExt.LocalPlayers.TryGetValue(conn, out var networkIdentity))
        {
            foreach (var player in NetworkManagerExt.LocalPlayers)
            {
                if (player.Key != conn)
                {
                    TargetPlayerConnected(conn, player.Value.GetComponent<InstanceInfo>());
                }
            }

            RpcPlayerConnected(networkIdentity.GetComponent<InstanceInfo>());
        }
    }

    [TargetRpc]
    private void TargetPlayerConnected(NetworkConnection conn, InstanceInfo info)
    {
        AddPlayer(info);
    }


    [ClientRpc]
    private void RpcPlayerConnected(InstanceInfo info)
    {
        AddPlayer(info);
    }

    private void AddPlayer(InstanceInfo info)
    {
        var playerView = Instantiate(_viewPrefab, _container);
        playerView.Init(info);
        _playersViews.Add(playerView);
    }
}
