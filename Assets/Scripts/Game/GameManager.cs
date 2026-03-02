using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public event Action<Player> ServerPlayerAdded;

    [SerializeField] private List<Transform> _spawnPoints = new();
    [SerializeField] private List<Transform> _itemsSpawnPoints = new();
    private List<Player> _players = new();
    private StateMachine _stateMachine;


    [field: SerializeField] public ItemFactory ItemFactory { get; private set; }
    [field:SerializeField] public GameConfig GameConfig { get; private set; }

    public ICollection<Player> Players => _players;
    public ICollection<Transform> SpawnPoints => _spawnPoints;

    public override void OnStartServer()
    {
        ItemFactory.Init(_itemsSpawnPoints);
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new PlayState(this, _stateMachine, GameConfig.PlayStateConfig));
        _stateMachine.SwitchState<PlayState>();
    }

    [ServerCallback]
    private void Update()
    {
        _stateMachine.Update();
    }

    [Server]
    public void AddPlayer(Player player)
    {
        _players.Add(player);
        ServerPlayerAdded?.Invoke(player);
    }
}
