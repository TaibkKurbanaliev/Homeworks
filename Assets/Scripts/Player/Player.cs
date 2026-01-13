using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private IInputService _input;
    private IMovementService _movement;
    private IHealth _health;

    public void Construct(IInputService inputService, IMovementService movement, IHealth health)
    {
        _input = inputService;
        _movement = movement;
        _health = health;
    }

    private void FixedUpdate()
    {
        _movement.Move(_input.GetMoveInput());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Collectable>(out _))
            Destroy(other.gameObject);
    }

    public void TakeDamage(float damage)
    {
        _health.ReduceHealth(damage);
    }
}
