using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable, IPauseEntity
{
    [SerializeField] private Vector3 _spawnPosition;
    private IInputService _input;
    private IMovementService _movement;
    private IHealth _health;
    private ILoggerService _logger;
    private bool _isPaused;

    public void Construct(IInputService inputService, IMovementService movement, IHealth health, ILoggerService logger)
    {
        _input = inputService;
        _movement = movement;
        _health = health;
        _logger = logger;
        _health.Died -= OnPlayerDied;
        _health.Died += OnPlayerDied;
        transform.position = _spawnPosition;
    }


    private void OnDisable()
    {
        if (_health != null)
            _health.Died -= OnPlayerDied;
    }

    private void FixedUpdate()
    {
        if (_isPaused)
            return;

        _movement.Move(_input.GetMoveInput());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Collectible>(out _))
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
        gameObject.SetActive(false);
    }

    public void Pause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
