using Mirror;
using System;
using System.Collections.Generic;

public class NetworkManagerExt : NetworkManager
{
    public static readonly Dictionary<NetworkConnection, NetworkIdentity> LocalPlayers = new Dictionary<NetworkConnection, NetworkIdentity>();
    public static new NetworkManagerExt singleton => (NetworkManagerExt) NetworkManager.singleton;
    public event Action OnServerPlayerConnected;
    public event Action OnServerPlayerDisconnected;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        var startPos = GetStartPosition();
        var player = startPos != null ?
                     Instantiate(playerPrefab, startPos.position, startPos.rotation) :
                     Instantiate(playerPrefab);

        LocalPlayers[conn] = player.GetComponent<NetworkIdentity>();
        NetworkServer.AddPlayerForConnection(conn, player);
        OnServerPlayerConnected?.Invoke();
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        OnServerPlayerDisconnected?.Invoke();
        LocalPlayers.Remove(conn);
        base.OnServerDisconnect(conn);
    }
}
