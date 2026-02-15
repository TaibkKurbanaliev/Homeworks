using Mirror;
using UnityEngine;

public class ClientInstance : NetworkBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerLobbyView _playerLobbyView;
}
