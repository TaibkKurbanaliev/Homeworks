using UnityEngine;

public class Pause : IState
{
    private bool _paused = false;
    private IStateSwitcher _stateSwitcher;

    public Pause(IStateSwitcher stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
    }

    public void Enter()
    {
        _paused = !_paused;
        EventBus.Instance.TriggerEvent(new GamePausedEvent(_paused ? "Game was Paused" : "Game was UnPaused", _paused));
    }

    public void Exit()
    {
    }

    public void Update()
    {
        if (!_paused)
        {
            _stateSwitcher.SwitchState<Playing>();
        }
    }
}
