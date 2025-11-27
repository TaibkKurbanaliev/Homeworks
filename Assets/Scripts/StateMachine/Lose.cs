using UnityEngine;

public class Lose : IState
{
    public void Enter()
    {
        EventBus.Instance.TriggerEvent(new GameLoseEvent("You Lose!!!"));
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}
