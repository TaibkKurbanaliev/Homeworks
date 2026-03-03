using Cysharp.Threading.Tasks;
using Mirror;
using System;
using System.Threading;
using UniExtension;
using Unity.Services.Analytics;
using UnityEngine;

public class PlayState : GameState
{
    private CancellationTokenSource _cts;
    private PlayStateConfig _cfg;
    private EventBinding<PlayerKilledEvent> _playerKilledEvent;

    private int _currentGameTime;

    public PlayState(GameManager gameManager, IStateSwitcher stateSwitcher, PlayStateConfig cfg) : base(gameManager, stateSwitcher)
    {
        _cfg = cfg;
        _playerKilledEvent = new EventBinding<PlayerKilledEvent>(OnPlayerKilled);
    }

    private void OnPlayerKilled(PlayerKilledEvent @event)
    {
        @event.Killer.PlayerInfo.AddKill();
    }

    public override void Enter()
    {
        base.Enter();

        _cts = new CancellationTokenSource();
        _currentGameTime = _cfg.GameTime;

        StartRespawnItems().Forget();
        StartTimer().Forget();

        GameManager.ServerPlayerAdded += OnServerPlayerAdded;
        EventBus<PlayerKilledEvent>.Register(_playerKilledEvent);
        
        foreach (var player in GameManager.Players)
        {
            player.ServerDied += OnServerDied;
        }
    }

    public override void Exit()
    {
        base.Exit();
        _cts.Cancel();
        GameManager.ServerPlayerAdded -= OnServerPlayerAdded;
        EventBus<PlayerKilledEvent>.Deregister(_playerKilledEvent);

        foreach (var player in GameManager.Players)
        {
            player.ServerDied -= OnServerDied;
        }
    }

    private void OnServerPlayerAdded(Player player)
    {
        player.ServerDied += OnServerDied;
    }

    private void OnServerDied(Player player)
    {
        player.PlayerInfo.AddDeath();
        StartRespawn(player).Forget();
    }

    private async UniTask StartRespawn(Player player)
    {
        await UniTask.WaitForSeconds(_cfg.RespawnDelay, cancellationToken: _cts.Token);
        player.Respawn(GameManager.SpawnPoints.GetRandomElement().position);
    }

    private async UniTask StartRespawnItems()
    {
        while (!_cts.IsCancellationRequested)
        {
            var itemType = Enum.GetNames(typeof(ItemType)).GetRandomElement();
            var item = GameManager.ItemFactory.Get(Enum.Parse<ItemType>(itemType));
            NetworkServer.Spawn(item.gameObject);
            await UniTask.WaitForSeconds(_cfg.ItemsRespawnDelay, cancellationToken: _cts.Token);
        }
    }

    private async UniTask StartTimer()
    {
        while (_currentGameTime > 0)
        {
            GameManager.Timer.SetTime(--_currentGameTime);
            await UniTask.WaitForSeconds(1f, cancellationToken: _cts.Token);
        }

        StateSwitcher.SwitchState<EndGameState>();
    }
}
