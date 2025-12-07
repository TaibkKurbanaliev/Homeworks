using UnityEngine;

public class WinState : IState
{
    public void Enter()
    {
        EventBus.Instance.TriggerEvent(new WinEvent());
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
