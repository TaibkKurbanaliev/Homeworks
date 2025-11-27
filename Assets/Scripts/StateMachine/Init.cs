using UnityEngine;

public class Init : IState
{
    private IStateSwitcher _switcher;

    public Init(IStateSwitcher switcher)
    {
        _switcher = switcher;
    }

    public void Enter()
    {
        EventBus.Instance.TriggerEvent(new GameInitEvent("Game is inited!"));
    }

    public void Exit()
    {
    }

    public void Update()
    {
        _switcher.SwitchState<Playing>();
    }
}
