using System;
using UnityEngine;

public class PlayingState : IState
{
    private IStateSwitcher _switcher;
    private Gameplay _gameplay;
    private int _currentWave = 1;

    public PlayingState(IStateSwitcher switcher, Gameplay gameplay)
    {
        _switcher = switcher;
        _gameplay = gameplay;
    }

    public void Enter()
    {
        EventBus.Instance.AddListener<PlayerDeathEvent>(OnPlayerDeath);
        EventBus.Instance.AddListener<AllEnemiesDiedEvent>(OnAllEnemiesDied);
    }


    public void Exit()
    {
        EventBus.Instance.RemoveListener<PlayerDeathEvent>(OnPlayerDeath);
        EventBus.Instance.RemoveListener<AllEnemiesDiedEvent>(OnAllEnemiesDied);
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        if (_currentWave > _gameplay.MaxWaves)
            _switcher.SwitchState<WinState>();
    }

    private void OnPlayerDeath(PlayerDeathEvent @event)
    {
        _switcher.SwitchState<LoseState>();
    }

    private void OnAllEnemiesDied(AllEnemiesDiedEvent @event)
    {
        _currentWave++;
        Debug.Log(_currentWave);
        if (_currentWave <= _gameplay.MaxWaves)
            EventBus.Instance.TriggerEvent(new NewWaveEvent());
    }
}
