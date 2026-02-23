using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerExt : NetworkManager
{
    public static readonly Dictionary<NetworkConnection, NetworkIdentity> LocalPlayers = new();
    public static new NetworkManagerExt singleton => (NetworkManagerExt)NetworkManager.singleton;

    public event Action<NetworkConnection> ServerPlayerConnected;
    public event Action<NetworkConnection> ServerPlayerDisconnected;
    public event Action ServerGameSceneLoaded;

    private static readonly Dictionary<NetworkConnection, PlayerInfo> _localPlayersInfo = new();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        var startPos = GetStartPosition();
        var player = startPos != null ?
                     Instantiate(playerPrefab, startPos.position, startPos.rotation) :
                     Instantiate(playerPrefab);

        var instanceInfo = player.GetComponent<InstanceInfo>();
        var newPlayerInfo = _localPlayersInfo.ContainsKey(conn) ? _localPlayersInfo[conn] : new PlayerInfo();

        NetworkServer.AddPlayerForConnection(conn, player);

        _localPlayersInfo[conn] = newPlayerInfo;
        LocalPlayers[conn] = player.GetComponent<NetworkIdentity>();
        instanceInfo.Init(newPlayerInfo);

        if (LocalPlayers.Count == 1)
            player.GetComponent<InstanceInfo>().SetLeader();

        ServerPlayerConnected?.Invoke(conn);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        ServerPlayerDisconnected?.Invoke(conn);
        LocalPlayers.Remove(conn);
        base.OnServerDisconnect(conn);
    }

    public override void ServerChangeScene(string newSceneName)
    {
        onlineScene = newSceneName;
        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerChangeScene(string newSceneName)
    {
        base.OnServerChangeScene(newSceneName);

        if (newSceneName == SceneName.Game.ToString())
        {
            ServerGameSceneLoaded?.Invoke();
        }

    }
}
