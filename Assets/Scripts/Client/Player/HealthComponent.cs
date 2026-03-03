using Mirror;
using System;
using UnityEngine;

public class HealthComponent : NetworkBehaviour
{
    public event Action Died;
    public event Action Respawned;
    public event Action ServerDied;

    [SerializeField] private float _maxHP;
    [SyncVar(hook = nameof(OnHealthChanged))] private float _currentHP;

    [ServerCallback]
    public void Awake()
    {
        _currentHP = _maxHP;
    }

    [Server]
    public void Reset()
    {
        _currentHP = _maxHP;
        RpcReset();
    }

    [ClientRpc]
    private void RpcReset()
    {
        if (!isOwned)
            return;

        Respawned?.Invoke();
    }

    [Server]
    public void TakeDamage(float damage, Player attacker)
    {
        if (_currentHP <= 0)
            return;

        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            ServerDied?.Invoke();

            if (attacker != null && attacker.Health != this)
                EventBus<PlayerKilledEvent>.Raise(new PlayerKilledEvent { Killer = attacker});
        }
    }

    [Server]
    public void Heal(float amount)
    {
        _currentHP = Mathf.Clamp(_currentHP + amount, 0, _maxHP);
    }

    [ClientRpc]
    public void RpcNotifyRespawn()
    {
        if (!isOwned)
            return;

        Respawned?.Invoke();
    }

    private void OnHealthChanged(float prev, float next)
    {
        if (!isOwned)
            return;

        EventBus<HealthChangedEvent>.Raise(new HealthChangedEvent { Value = Math.Clamp(next, 0, _maxHP) / _maxHP });

        if (next <= 0)
            Died?.Invoke();
    }
}
