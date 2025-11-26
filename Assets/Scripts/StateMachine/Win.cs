using UnityEngine;

public class Win : IState
{
    public void Enter()
    {
        EventBus.Instance.TriggerEvent(new GamePausedEvent("Game Win!!!", true));
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}
