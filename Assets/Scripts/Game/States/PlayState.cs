using Cysharp.Threading.Tasks;
using System;
using UniExtension;
using Unity.Services.Analytics;
using UnityEngine;

public class PlayState : GameState
{
    private PlayStateConfig _cfg;

    public PlayState(GameManager gameManager, IStateSwitcher stateSwitcher, PlayStateConfig cfg) : base(gameManager, stateSwitcher)
    {
        _cfg = cfg;
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.ServerPlayerAdded += OnServerPlayerAdded;
        
        foreach (var player in GameManager.Players)
        {
            player.ServerDied += OnServerDied;
        }
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.ServerPlayerAdded -= OnServerPlayerAdded;

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
        StartRespawn(player).Forget();
    }

    private async UniTask StartRespawn(Player player)
    {
        await UniTask.WaitForSeconds(_cfg.RespawnDelay);
        player.Respawn(GameManager.SpawnPoints.GetRandomElement().position);
    }
}
