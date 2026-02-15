using Mirror;
using System;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    [SerializeField] private PlayerLobbyView _viewPrefab;
    [SerializeField] private Transform _container;

    private List<PlayerLobbyView> _playersViews = new List<PlayerLobbyView>();

    private void Awake()
    {
        NetworkManagerExt.singleton.OnServerPlayerConnected += Singleton_OnServerPlayerConnected; ;
        NetworkManagerExt.singleton.OnServerPlayerDisconnected += Singleton_OnServerPlayerDisconnected; ;
    }

    private void Singleton_OnServerPlayerDisconnected()
    {
        throw new NotImplementedException();
    }

    private void Singleton_OnServerPlayerConnected(NetworkConnection conn)
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
