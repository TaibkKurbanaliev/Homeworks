using Mirror;
using UnityEngine;

public class ClientInstance : NetworkBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerLobbyView _playerLobbyView;

    private static ClientInstance _instance;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        _instance = this;
    }

    public static ClientInstance ReturnClientInstance(NetworkConnection conn = null)
    {
        if (NetworkClient.active && conn != null)
        {
            NetworkIdentity localPlayer;

            if (NetworkManagerExt.LocalPlayers.TryGetValue(conn, out localPlayer))
            {
                return localPlayer.GetComponent<ClientInstance>();
            }
            else
            {
                return null;
            }
        }

        return _instance;
    }
}
