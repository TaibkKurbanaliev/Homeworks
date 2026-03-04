using UnityEngine;

public class DyingState : IState
{
    private Player _player;
    private IStateSwitcher _stateSwitcher;
    private DyingStateConfig _cfg;
    private float _verticalAngle;

    public DyingState(Player player, IStateSwitcher stateSwitcher, DyingStateConfig cfg)
    {
        _player = player;
        _stateSwitcher = stateSwitcher;
        _cfg = cfg;
    }

    public void Enter()
    {
        _player.PlayerView.SetDeath();
        _player.CharacterController.enabled = false;
    }

    public void Exit()
    {
        _player.PlayerView.SetAlive();
        _player.CharacterController.enabled = true;
    }

    public void FixedUpdate()
    {
    }

    public void HandleInput()
    {
    }

    public void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if (_player.Input.Move() == Vector2.zero)
            return;

        var spectator = _player.CameraGhostTarget.transform;
        var targetPos = _cfg.SpectatorSpeed * Time.deltaTime * 
                        (spectator.right * _player.Input.Move().x + spectator.forward * _player.Input.Move().y);
        spectator.position += targetPos;
    }

    private void Rotate()
    {
        _player.CameraGhostTarget.transform.Rotate(Vector3.up, _player.Input.Look().x * _cfg.Sensetive * Time.deltaTime);

        _verticalAngle -= _player.Input.Look().y * _cfg.Sensetive * Time.deltaTime;
        _verticalAngle = Mathf.Clamp(_verticalAngle, -89f, 89f);

        Vector3 euler = _player.CameraGhostTarget.transform.localEulerAngles;
        euler.x = _verticalAngle;
        _player.CameraGhostTarget.transform.localEulerAngles = euler;
    }
}
