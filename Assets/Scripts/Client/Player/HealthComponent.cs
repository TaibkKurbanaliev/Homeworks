using Mirror;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HealthComponent : NetworkBehaviour
{
    [SerializeField] private float _maxHP;
    [SyncVar(hook = nameof(OnHealthChanged))] private float _currentHP;

    [ServerCallback]
    public void Awake()
    {
        _currentHP = _maxHP;
    }

    [Server]
    public void TakeDamage(float damage)
    {
        if (_currentHP <= 0) 
            return;

        _currentHP -= damage;
    }

    private void OnHealthChanged(float prev, float next)
    {
        if (!isOwned)
            return;

        EventBus<HealthChangedEvent>.Raise(new HealthChangedEvent { Value = Math.Clamp(next, 0, _maxHP) / _maxHP });

        if (next <= 0)
            EventBus<DiedEvent>.Raise(new DiedEvent());
    }
}
