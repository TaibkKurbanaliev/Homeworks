using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable, IPauseEntity
{
    [SerializeField] private Vector3 _spawnPosition;
    public IInputService Input { get; private set; }
    private IMovementService _movement;
    private IHealth _health;
    private ILoggerService _logger;
    private bool _isPaused;
    private List<IGameModifier> _modifiers;

    public void Construct(IInputService inputService, IMovementService movement, IHealth health,
                          ILoggerService logger, List<IGameModifier> modifiers)
    {
        Input = inputService;
        _movement = movement;
        _health = health;
        _logger = logger;
        _health.Died -= OnPlayerDied;
        _health.Died += OnPlayerDied;
        transform.position = _spawnPosition;
        _modifiers = modifiers;
    }

    public void ChangeInput(IInputService input)
    {
        Input = input;
    }

    private void OnDisable()
    {
        if (_health != null)
            _health.Died -= OnPlayerDied;
    }

    private void Update()
    {
        if (_isPaused)
            return;

        foreach (var modifier in _modifiers)
            modifier.Tick(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_isPaused)
            return;

        _movement.Move(Input.GetMoveInput());
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
