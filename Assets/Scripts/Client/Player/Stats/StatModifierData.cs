using System;
using UnityEngine;

[Serializable]
public struct StatModifierData
{
    public int ID;
    public StatType StatType;
    public OperationType OperationType;
    public float Value;

    private static int _id;

    public StatModifierData(StatType statType, OperationType operationType, float value)
    {
        StatType = statType;
        OperationType = operationType;
        Value = value;
        ID = _id++;
    }
}
