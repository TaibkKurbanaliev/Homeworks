using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : NetworkBehaviour
{
    [SerializeField] private PlayerLeaderboardView _prefab;

    private List<PlayerLeaderboardView> _views = new();
    private EventBinding<ServerPlayerConnectedToGame> _serverPlayerConnectBinding;

    private void Awake()
    {
        _serverPlayerConnectBinding = new EventBinding<ServerPlayerConnectedToGame>(OnServerPlayerConnectedToGame);
        EventBus<ServerPlayerConnectedToGame>.Register(_serverPlayerConnectBinding);
    }

    private void OnDestroy()
    {
        EventBus<ServerPlayerConnectedToGame>.Deregister(_serverPlayerConnectBinding);
    }

    private void OnServerPlayerConnectedToGame(ServerPlayerConnectedToGame player)
    {
        AddPlayer(player.PlayerInfo, player.InstanceInfo);
    }

    [Server]
    private void AddPlayer(PlayerInfo playerInfo, InstanceInfo instanceInfo)
    {
        RpcAddPlayer(playerInfo, instanceInfo);
    }

    [ClientRpc]
    private void RpcAddPlayer(PlayerInfo playerInfo, InstanceInfo instanceInfo)
    {
        var view = Instantiate(_prefab, transform);
        view.Init(playerInfo, instanceInfo);
        _views.Add(view);
    }
}
