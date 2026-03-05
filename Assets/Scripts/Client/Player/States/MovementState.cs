using System;
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
        Player.Health.Died += OnDied;
        Player.Input.ReloadPressed += OnReloadPressed;
    }

    public virtual void Exit()
    {
        Player.Health.Died -= OnDied;
        Player.Input.ReloadPressed -= OnReloadPressed;
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void HandleInput()
    {
        Data.MoveInput = Player.Input.Move();
        Data.LookInput = Player.Input.Look();
    }

    public virtual void Update()
    {
        Rotate();
        Move();
        Gravity();
        Fire();
    }

    private void OnDied()
    {
        StateSwitcher.SwitchState<DyingState>();
    }

    private void Move()
    {
        var moveDir = Player.transform.forward * Data.MoveInput.y + Player.transform.right * Data.MoveInput.x;
        var newVelocity = moveDir * Data.HorizontalSpeed * Time.deltaTime;
        Data.Velocity.x = newVelocity.x;
        Data.Velocity.z = newVelocity.z;
        Player.CharacterController.Move(Data.Velocity * Time.deltaTime);
        Player.PlayerView.SetAnimDirection(Data.MoveInput);
    }

    private void Rotate()
    {
        Player.transform.Rotate(Player.transform.up, Data.LookInput.x * Player.Settings.MouseSensetive * Time.deltaTime);
        Player.PlayerView.Rotate(Data.LookInput.y);

    }

    private void Gravity()
    {
        if (!Player.CharacterController.isGrounded)
            Data.Velocity.y += Physics.gravity.y * Time.deltaTime;
        else
            Data.Velocity.y = Physics.gravity.y;
    }

    private void Fire()
    {
        if (Player.Input.IsFire()) 
            Player.Weapon.Shoot();
    }

    private void OnReloadPressed()
    {
        Player.Weapon.Reload();
    }
}
