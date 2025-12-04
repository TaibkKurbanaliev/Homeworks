using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IDamagable
{
    private StateMachine _stateMachine;

    [field: SerializeField] public Weapon Weapon { get; private set; }
    [field: SerializeField] public PlayerConfig Config { get; private set; }
    [field: SerializeField] public PlayerView View {  get; private set; }
    public CharacterController Controller {  get; private set; }
    public PlayerInputActions Input {  get; private set; }
    public Health Health { get; private set; }

    public void Init(PlayerInputActions input)
    {
        Controller = GetComponent<CharacterController>();
        Health = new(Config.Health);
        Input = input;

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new IdleState(this, _stateMachine));
        _stateMachine.AddState(new WalkState(this, _stateMachine));
        _stateMachine.AddState(new DeathState(this));
        _stateMachine.SwitchState<IdleState>();
    }

    private void Update() => _stateMachine.Update();
    private void FixedUpdate() => _stateMachine.FixedUpdate();

    public void TakeDamage(float damage, Vector3 hitNormal)
    {
        if (damage  <= 0) return;

        Health.ReduceHealth(damage);
        EventBus.Instance.TriggerEvent(new PlayerHealthChangeEvent("Health changed", Health.HealthPercent));
    }
}
