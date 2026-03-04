using System;
using UnityEngine;

[Serializable]
public class WalkingStateConfig
{
    [SerializeField] private float _speed;

    private StatMediator _statMediator;

    [field: SerializeField] public float Drag;

    public float Speed => _statMediator.Get(_speed, StatType.Speed);

    public void Init(StatMediator statMediator)
    {
        _statMediator = statMediator;
    }
}
