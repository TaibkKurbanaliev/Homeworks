using UnityEngine;

public class Playing : IState
{
    private IStateSwitcher _stateSwitcher;
    private GameStateService _service;

    public Playing(IStateSwitcher stateSwitcher, GameStateService service)
    {
        _stateSwitcher = stateSwitcher;
        _service = service;
    }

    public void Enter()
    {
        EventBus.Instance.OnGameEvent += HandleEvent;
    }

    public void Exit()
    {
        EventBus.Instance.OnGameEvent -= HandleEvent;
    }

    public void Update()
    {
        if (_service.CurrentScore == _service.WinScore)
        {
            _stateSwitcher.SwitchState<Win>();
        }
    }

    private void HandleEvent(IEvent @event)
    {
        switch (@event)
        {
            case ItemPickedEvent item:
                _service.AddScore(item.Score);
                EventBus.Instance.TriggerEvent(new ScoreChanged(_service.CurrentScore, "Score was changed!!!"));
                break;
        }
    }
}
