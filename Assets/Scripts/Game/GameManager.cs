using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private List<Player> _players = new();
    private StateMachine _stateMachine;

    [field:SerializeField] public GameConfig GameConfig { get; private set; }

    public IReadOnlyCollection<Player> Players => _players;

    private void Awake()
    {
        //_stateMachine.AddState(new EnterState());
    }

    public void AddPlayer(Player _player)
    {
        _players.Add(_player);
    }
}
