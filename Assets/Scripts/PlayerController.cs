using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private GunConfig _gunCfg;

    private PlayerInputActions _inputActions;

    public Gun Gun => _gun;

    public void Init(PlayerInputActions inputActions)
    {
        _inputActions = inputActions;
        _inputActions.Player.Attack.performed += OnAttack;
        _gun.Init(_gunCfg);

        if (_gunCfg is null)
            Debug.LogError("Gun config isn't assigned");
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        _gun.Fire();
    }
}
