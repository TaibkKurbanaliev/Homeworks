using UnityEngine;

public abstract class MovementState : IState
{
    protected Player Player { get; private set; }
    protected IStateSwitcher StateSwitcher { get; private set; }
    protected StatesData Data { get; private set; }

    public MovementState(Player player, IStateSwitcher stateSwitcher, StatesData statesData)
    {
        Player = player;
        StateSwitcher = stateSwitcher;
        Data = statesData;
    }

    public virtual void Enter()
    {
        Debug.Log($"Enter the {GetType().Name}");
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
        GroundCheck();
        Move();
    }

    public virtual void HandleInput()
    {
        Data.Input = Player.Input.Move();
    }

    public virtual void Update()
    {
        Rotate();
    }

    private void Move()
    {
        var moveDir = Player.transform.forward * Data.Input.y + Player.transform.right * Data.Input.x;
        var newVelocity = moveDir * Data.HorizontalSpeed;
        Player.Rigidbody.AddForce(newVelocity, ForceMode.VelocityChange);
    }

    private void Rotate()
    {

    }

    private void GroundCheck()
    {
        Vector3 playerCenter = Player.Rigidbody.position + Player.Collider.center;
        var castRadiusOffset = 0.01f;

        var distance = (Player.Collider.height / 2) - Player.Collider.radius;
        var ray = new Ray(playerCenter, -Player.transform.up); // down direction

        if (Physics.SphereCast(ray, Player.Collider.radius - castRadiusOffset, out var hitInfo, distance + castRadiusOffset * 2f))
        {
            Data.IsGrounded = true;
            return;
        }

        Data.IsGrounded = false;
    }
}
