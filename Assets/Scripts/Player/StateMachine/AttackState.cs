using UnityEngine;

public class AttackState : IState
{
    private IStateSwitcher _stateSwitcher;
    private Player _player;

    private float _delay;

    public AttackState(IStateSwitcher stateSwitcher, Player player)
    {
        _stateSwitcher = stateSwitcher;
        _player = player;
    }

    public void Enter()
    {
        _player.PlayerView.SetAttackState();
    }

    public void Exit()
    {
        _delay = 0;
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        if (_delay >= _player.Config.AttackTime)
        {
            var hit = Physics2D.Raycast(_player.transform.position,
                                        _player.transform.right,
                                        _player.Config.AttackRange,
                                        _player.Target);

            if (hit.collider != null && hit.collider.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage(_player.Config.Damage);

            _stateSwitcher.SwitchState<IdleState>();
        }

        _delay += Time.deltaTime;
    }
}
