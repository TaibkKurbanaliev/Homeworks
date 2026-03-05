using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputSystem : IInput, IDisposable
{
    public event Action Jumped;
    public event Action TabOpenned;
    public event Action TabClosed;
    public event Action Healed;
    public event Action GrenadeThrowed;
    public event Action ReloadPressed;

    private InputSystem_Actions _actions;

    public NewInputSystem()
    {
        _actions = new InputSystem_Actions();
        _actions.Enable();
        _actions.Player.Heal.started += OnHealStarted;
        _actions.Player.Jump.started += OnJumpStarted;
        _actions.Player.ThrowGranade.started += OnThrowGranadeStarted;
        _actions.Player.Reload.started += OnReloadStarted;
        _actions.UI.OpenTab.started += OnTabOpenned;
        _actions.UI.OpenTab.canceled += OnTabClosed;
    }

    private void OnReloadStarted(InputAction.CallbackContext context) => ReloadPressed?.Invoke();
    private void OnThrowGranadeStarted(InputAction.CallbackContext context) => GrenadeThrowed?.Invoke();
    private void OnTabClosed(InputAction.CallbackContext context) => TabClosed?.Invoke();
    private void OnTabOpenned(InputAction.CallbackContext context) => TabOpenned?.Invoke();
    private void OnJumpStarted(InputAction.CallbackContext context) => Jumped?.Invoke();
    private void OnHealStarted(InputAction.CallbackContext context) => Healed?.Invoke();
    public Vector2 Look() => _actions.Player.Look.ReadValue<Vector2>();

    public Vector2 Move() => _actions.Player.Move.ReadValue<Vector2>();

    public void Dispose()
    {
        _actions.Disable();
        _actions.Player.Heal.started -= OnHealStarted;
        _actions.Player.Jump.started -= OnJumpStarted;
        _actions.Player.ThrowGranade.started -= OnThrowGranadeStarted;
        _actions.UI.OpenTab.started -= OnTabOpenned;
        _actions.UI.OpenTab.canceled -= OnTabClosed;
    }


    public bool IsFire() => _actions.Player.Attack.IsPressed();
}
