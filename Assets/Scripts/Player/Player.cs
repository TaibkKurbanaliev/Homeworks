using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private LayerMask _doorLayerMask;
    [SerializeField] private List<Weapon> _weapons;
    
    private StateMachine _stateMachine;
    private int _currentWeaponIndex;
    private Task _reloadTask;

    [field: SerializeField] public PlayerConfig Config { get; private set; }
    [field: SerializeField] public PlayerView View { get; private set; }
    
    public CharacterController Controller { get; private set; }
    public Weapon CurrentWeapon { get; private set; }
    public PlayerInputActions Input { get; private set; }
    public Health Health { get; private set; }

    public void Init(PlayerInputActions input)
    {
        Controller = GetComponent<CharacterController>();
        Health = new(Config.Health);
        Input = input;

        CurrentWeapon = _weapons[0];

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new IdleState(this, _stateMachine));
        _stateMachine.AddState(new WalkState(this, _stateMachine));
        _stateMachine.AddState(new DeathState(this));
        _stateMachine.SwitchState<IdleState>();
    }

    public void OnEnable()
    {
        Input.Player.NextWeapon.performed += OnSwitchWeapon;
        Input.Player.Reload.started += OnReload;
        EventBus.Instance.TriggerEvent(new SwapWeaponEvent(CurrentWeapon.Config.Icon, CurrentWeapon.CurrentNumberOfBullets));
    }

    private void OnReload(InputAction.CallbackContext context)
    {
        if (_reloadTask is null || _reloadTask.IsCompleted)
        {
            _reloadTask = CurrentWeapon.Reload();
            Debug.Log("Reload");
        }
    }

    public void OnDisable()
    {
        Input.Player.Reload.started -= OnReload;
        Input.Player.NextWeapon.performed -= OnSwitchWeapon;
    }

    private void Update() => _stateMachine.Update();
    private void FixedUpdate() => _stateMachine.FixedUpdate();
    public void OnCollisionEnter(Collision collision)
    {
        if ((_doorLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            var forceToOpenDoor = 100f;
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForceAtPosition(-collision.contacts[0].normal * forceToOpenDoor, collision.contacts[0].point);
            }
        }
    }

    public void TakeDamage(float damage, Vector3 hitNormal)
    {
        if (damage <= 0) return;

        Health.ReduceHealth(damage);
        EventBus.Instance.TriggerEvent(new PlayerHealthChangeEvent("Health changed", Health.HealthPercent));
    }

    private void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        _currentWeaponIndex = Math.Abs((_currentWeaponIndex + (int)context.ReadValue<Vector2>().y) % _weapons.Count);
        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeapon = _weapons[_currentWeaponIndex];
        CurrentWeapon.gameObject.SetActive(true);
        EventBus.Instance.TriggerEvent(new SwapWeaponEvent(CurrentWeapon.Config.Icon, CurrentWeapon.CurrentNumberOfBullets));
    }
}
