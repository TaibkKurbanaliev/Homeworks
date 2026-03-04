using System;
using UnityEngine;

public enum OperationType
{
    Add,
    Substract,
    Multiply
}

public class StatModifier
{
    public StatType StatType { get; private set; }
    private OperationType _operationType;
    private float _value;

    public StatModifier(StatType statType, OperationType operationType, float value)
    {
        StatType = statType;
        _operationType = operationType;
        _value = value;
    }

    public float Get(float value)
    {
        switch (_operationType)
        {
            case OperationType.Add:
                return value + _value;
            case OperationType.Substract:
                return value - _value;
            case OperationType.Multiply:
                return value * _value;
            default:
                throw new NotImplementedException();
        }
    }
}