using System;
using UnityEngine;

public class PlayerView
{
    private const string MoveTrigger = "Move";
    private const string IdleTrigger = "Idle";
    private const string AttackTrigger = "Attack";

    private Animator _animator;

    public PlayerView(Animator animator)
    {
        _animator = animator;
    }

    public void SetMovementAnimation()
    {
        _animator.SetTrigger(MoveTrigger);
    }

    public void SetIdleAnimation()
    {
        _animator.SetTrigger(IdleTrigger);
    }

    internal void SetAttackState()
    {
        _animator.SetTrigger(AttackTrigger);
    }
}
