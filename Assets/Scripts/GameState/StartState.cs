using UnityEngine;

public class StartState : IState
{
    private IStateSwitcher _switcher;

    public StartState(IStateSwitcher switcher)
    {
        _switcher = switcher;
    }

    public void Enter()
    {
        Debug.Log("Start Game");
        EventBus.Instance.TriggerEvent(new NewWaveEvent());
    }

    public void Exit()
    {}

    public void FixedUpdate()
    {}

    public void Update()
    {
        _switcher.SwitchState<PlayingState>();
    }
}
