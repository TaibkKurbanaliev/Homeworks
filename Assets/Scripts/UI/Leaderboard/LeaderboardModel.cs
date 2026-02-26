using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardModel : NetworkBehaviour
{
    public SyncList<ServerPlayerConnectedToGame> ServerPlayers = new();
    
    private EventBinding<ServerPlayerConnectedToGame> _serverPlayerConnectBinding;

    public override void OnStartServer()
    {
        _serverPlayerConnectBinding = new EventBinding<ServerPlayerConnectedToGame>(OnServerPlayerConnectedToGame);
        EventBus<ServerPlayerConnectedToGame>.Register(_serverPlayerConnectBinding);
    }

    public override void OnStopServer()
    {
        EventBus<ServerPlayerConnectedToGame>.Deregister(_serverPlayerConnectBinding);
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
}
