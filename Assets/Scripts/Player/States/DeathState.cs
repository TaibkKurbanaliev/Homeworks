using UnityEngine;

public class DeathState : IState
{
    private Player _player;

    public DeathState(Player player)
    {
        _player = player;
    }

    public void Enter()
    {
        _player.View.PlayDeathAnim();
        _player.Input.Disable();
        EventBus.Instance.TriggerEvent(new PlayerDeathEvent("Player died"));
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
    }
}
