using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private IInputService _input;
    private IMovementService _movement;
    private IHealth _health;
    private ILoggerService _logger;

    public void Construct(IInputService inputService, IMovementService movement, IHealth health, ILoggerService logger)
    {
        _input = inputService;
        _movement = movement;
        _health = health;
        _logger = logger;
        _health.Died -= OnPlayerDied;
        _health.Died += OnPlayerDied;
    }


    private void OnDisable()
    {
        _health.Died -= OnPlayerDied;
    }

    private void FixedUpdate()
    {
        _movement.Move(_input.GetMoveInput());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Collectable>(out _))
        {
            _logger.Log("Item picked");
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        _health.ReduceHealth(damage);
    }

    private void OnPlayerDied()
    {
        Destroy(gameObject);
    }
}
