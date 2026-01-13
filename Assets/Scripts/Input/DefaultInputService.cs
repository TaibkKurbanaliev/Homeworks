using System;
using UnityEngine;

public class DefaultInputService : IInputService
{
    private InputSystem_Actions _actions;

    public DefaultInputService(InputSystem_Actions actions)
    {
        _actions = actions;
    }

    public Vector2 GetMoveInput()
    {
        return _actions.Player.Move.ReadValue<Vector2>();
    }

    public bool WasActionPressed()
    {
        return _actions.Player.Interact.WasPressedThisFrame();
    }
}
