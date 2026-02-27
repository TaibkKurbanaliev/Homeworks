using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardModel : NetworkBehaviour
{
    public SyncList<ServerPlayerConnectedToGame> ServerPlayers = new();
    
    private EventBinding<ServerPlayerConnectedToGame> _serverPlayerConnectBinding;
    private EventBinding<ServerPlayerDisconnected> _serverPlayerDisconnectedBinding;

    public override void OnStartServer()
    {
        _serverPlayerConnectBinding = new EventBinding<ServerPlayerConnectedToGame>(OnServerPlayerConnectedToGame);
        _serverPlayerDisconnectedBinding = new EventBinding<ServerPlayerDisconnected>(OnServerPlayerDisconnected);
        EventBus<ServerPlayerConnectedToGame>.Register(_serverPlayerConnectBinding);
        EventBus<ServerPlayerDisconnected>.Register(_serverPlayerDisconnectedBinding);
    }
    public override void OnStopServer()
    {
        EventBus<ServerPlayerConnectedToGame>.Deregister(_serverPlayerConnectBinding);
        EventBus<ServerPlayerDisconnected>.Deregister(_serverPlayerDisconnectedBinding);
    }

    private void OnServerPlayerDisconnected(ServerPlayerDisconnected disconnected)
    {
        DeletePlayer(disconnected);
    }

    private void OnServerPlayerConnectedToGame(ServerPlayerConnectedToGame player)
    {
        AddPlayer(player);
    } 

    [Server]
    private void AddPlayer(ServerPlayerConnectedToGame player)
    {
        ServerPlayers.Add(player);
    }

    [Server]
    private void DeletePlayer(ServerPlayerDisconnected player)
    {
        var item = ServerPlayers.FirstOrDefault(serverPlayer => serverPlayer.NetID == player.NetID);
        ServerPlayers.Remove(item);
    }
}
