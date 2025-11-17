using UnityEngine;

public class EnemyView
{
    private const string RunningTrigger = "Running";
    private const string IdlingTrigger = "Idling";
    private const string FightingTrigger = "Fighting";
    private const string DeathTrigger = "Death";

    private Animator _animator;

    public EnemyView(Animator animator)
    {
        _animator = animator;
    }

    public void SetSearching() => _animator.SetTrigger(IdlingTrigger);

    public void SetFighting() => _animator.SetTrigger(FightingTrigger);

    public void SetChasing() => _animator.SetTrigger(RunningTrigger);

    public void SetDeath() => _animator.SetTrigger(DeathTrigger);
}
