using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [field: SerializeField] protected float Interval { get; private set; }

    public abstract void ApplyEffect(Player player);
    public abstract void StopEffect(Player player);
}
