using System;
using UnityEngine;

[Serializable]
public class AirborneStateConfig
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _airHorizontalSpeed;

    private StatMediator _mediator;

    public float JumpForce => _mediator.Get(_jumpForce, StatType.JumpForce);
    public float AirHorizontalSpeed => _mediator.Get(_airHorizontalSpeed, StatType.Speed);

    public void Init(StatMediator mediator)
    {
        _mediator = mediator;
    }
}
