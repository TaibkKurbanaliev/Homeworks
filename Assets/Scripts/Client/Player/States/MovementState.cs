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

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void HandleInput()
    {
        Data.Input = Player.Actions.Player.Move.ReadValue<Vector2>();
    }

    public void Update()
    {
        Rotate();
    }

    private void Move()
    {
        var moveDir = Player.transform.forward * Data.Input.y + Player.transform.right * Data.Input.x;
        var newVelocity = moveDir * Data.Speed;
        Player.Rigidbody.AddForce(newVelocity, ForceMode.VelocityChange);
    }

    private void Rotate()
    {

    }
}
