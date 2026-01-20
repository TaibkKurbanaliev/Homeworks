using System;
using UnityEngine;

public class DefaultInputService : IInputService
{
    private InputSystem_Actions _actions;
    private bool _enabled = true;

    public DefaultInputService(InputSystem_Actions actions)
    {
        _actions = actions;
    }

    public Vector2 GetMoveInput()
    {
        if (!_enabled) 
            return Vector2.zero;

        return _actions.Player.Move.ReadValue<Vector2>();
    }

    public bool IsActionPressed()
    {
        return _actions.Player.Interact.WasPressedThisFrame();
    }

    public void SetActive(bool isEnabled)
    {
        _enabled = isEnabled;
    }
}
