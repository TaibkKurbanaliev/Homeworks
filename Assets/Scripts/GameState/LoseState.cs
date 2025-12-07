using UnityEngine;

public class LoseState : IState
{
    public void Enter()
    {
        EventBus.Instance.TriggerEvent(new LoseEvent());
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
    }
}
