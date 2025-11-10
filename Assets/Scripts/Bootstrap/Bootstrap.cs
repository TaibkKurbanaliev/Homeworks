using System;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayModeManager _playModeManager;

    private PlayerInputActions _actions;
    protected PlayerInputActions PlayerInputActions => _actions;

    public void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _actions = new PlayerInputActions();
        _actions.Enable();
        _playModeManager.Init(_actions);
    }

    private void OnDisable()
    {
        _actions.Disable();
    }
}
