using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputSystem : IInput, IDisposable
{
    public event Action Jumped;

    private InputSystem_Actions _actions;

    public NewInputSystem()
    {
        _actions = new InputSystem_Actions();
        _actions.Enable();
        _actions.Player.Jump.started += OnJumpStarted;
    }

    public Vector2 Look() => _actions.Player.Look.ReadValue<Vector2>();

    public Vector2 Move() => _actions.Player.Move.ReadValue<Vector2>();

    public void Dispose()
    {
        _actions.Disable();
        _actions.Player.Jump.started -= OnJumpStarted;
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        Jumped?.Invoke();
    }
}
