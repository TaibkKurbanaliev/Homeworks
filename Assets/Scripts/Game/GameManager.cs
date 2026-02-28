using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public event Action<Player> ServerPlayerAdded;

    [SerializeField] private List<Transform> _spawnPoints = new();
    private List<Player> _players = new();
    private StateMachine _stateMachine;

    [field:SerializeField] public GameConfig GameConfig { get; private set; }

    public ICollection<Player> Players => _players;
    public ICollection<Transform> SpawnPoints => _spawnPoints;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new PlayState(this, _stateMachine, GameConfig.PlayStateConfig));
        _stateMachine.SwitchState<PlayState>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
        ServerPlayerAdded?.Invoke(player);
    }
}
