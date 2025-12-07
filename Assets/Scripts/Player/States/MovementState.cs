using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class MovementState : IState
{
    protected float Speed;
    protected float Acceleration;
    protected float Deceleration;

    private Vector3 _targetDir;
    private float _deathPoint = -30f;

    public MovementState(Player player, IStateSwitcher switcher)
    {
        Player = player;
        Switcher = switcher;
    }

    protected Player Player { get; private set; }
    protected IStateSwitcher Switcher { get; private set; }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Update()
    {
        Rotate();
        Gravity();
        Move();

        if (!Player.Health.IsAlive || Player.transform.position.y < _deathPoint)
            Switcher.SwitchState<DeathState>();
    }

    private void Move()
    {
        var input = Player.Input.Player.Move.ReadValue<Vector2>();
        var moveDir = Player.transform.forward * input.y + Player.transform.right * input.x;

        Vector3 newVelocity;
        var currentHorizontalVelocity = new Vector3(Player.Controller.velocity.x, 0f, Player.Controller.velocity.z);

        if (input != Vector2.zero)
        {
            var movementDelta = moveDir * Acceleration * Time.deltaTime;
            newVelocity = movementDelta + currentHorizontalVelocity;
        }
        else
        {
            newVelocity = Vector3.Lerp(
                currentHorizontalVelocity,
                Vector3.zero,
                Deceleration * Time.fixedDeltaTime);
        }

        newVelocity = Vector3.ClampMagnitude(newVelocity, Speed);

        _targetDir = new Vector3(newVelocity.x, _targetDir.y, newVelocity.z);

        Player.Controller.Move(_targetDir * Time.deltaTime);
        Player.View.SetDirection(input);
    }

    public void Rotate()
    {
        Player.View.LookAtMouse();
    }

    private void Gravity()
    {
        if (Player.Controller.isGrounded)
        {
            _targetDir.y += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _targetDir.y = Physics.gravity.y;
        }
    }
}
