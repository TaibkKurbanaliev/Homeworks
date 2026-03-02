using Cysharp.Threading.Tasks;
using Mirror;
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
        StartRespawnItems().Forget();
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

    private async UniTask StartRespawnItems()
    {
        while (true)
        {
            var itemType = Enum.GetNames(typeof(ItemType)).GetRandomElement();
            var item = GameManager.ItemFactory.Get(Enum.Parse<ItemType>(itemType));
            NetworkServer.Spawn(item.gameObject);
            await UniTask.WaitForSeconds(_cfg.ItemsRespawnDelay);
        }
    }
}
