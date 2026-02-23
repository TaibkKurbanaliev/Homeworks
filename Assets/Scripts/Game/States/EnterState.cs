using Cysharp.Threading.Tasks;
using Mirror;
using UnityEngine;

public class EnterState : GameState
{
    public EnterState(GameManager gameManager, IStateSwitcher stateSwitcher) : base(gameManager, stateSwitcher)
    {

    }

    public override void Enter()
    {
        WaitAllPlayersConnect().Forget();
    }

    private async UniTask WaitAllPlayersConnect()
    {
        await UniTask.WaitUntil(() => GameManager.Players.Count == NetworkServer.connections.Count);
    }

    private async UniTask StartTimer()
    {/*
        GameManager.Start*/
    }
}
