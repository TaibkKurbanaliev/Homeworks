using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class StatMediator : NetworkBehaviour
{
    public Dictionary<StatModifierData, StatModifier> _statModifiers = new();

    [TargetRpc]
    public void AddModifier(NetworkConnection conn, StatModifierData modifier)
    {
        _statModifiers[modifier] = new StatModifier(modifier.StatType, modifier.OperationType, modifier.Value);
    }

    [TargetRpc]
    public void RemoveModifier(NetworkConnection conn, StatModifierData modifier)
    {
        _statModifiers.Remove(modifier);
    }

    public float Get(float value, StatType type)
    {
        foreach (var modifier in _statModifiers)
        {
            if (modifier.Value.StatType == type)
                value = modifier.Value.Get(value);
        }

        return value;
    }
}
