using System.Linq;
using UnityEngine;

public class EndGameState : GameState
{
    public EndGameState(GameManager gameManager, IStateSwitcher stateSwitcher) : base(gameManager, stateSwitcher)
    {
    }

    public override void Enter()
    {
        base.Enter();

        EventBus<GameEndedEvent>.Raise(new GameEndedEvent());

        foreach (var player in GameManager.Players)
        {
            var conn = NetworkManagerExt.LocalPlayers.FirstOrDefault(sp =>
                                                          sp.Value.GetComponent<ClientInstance>().CurrentPlayer == player).Key;

            player.TargetRpcDisableInput(conn);
        }

        GameManager.ShowEndGame();
    }
}
