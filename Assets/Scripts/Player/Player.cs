using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    private PlayerInputActions _input;
    private EntityView _playerView;
    private PlayerMovement _movement;

    private Vector2 _moveInput;
    private bool _isPaused = false;

    public void Init(PlayerInputActions input, float moveSpeed)
    {
        _input = input;
        _movement = new PlayerMovement(_rigidbody, moveSpeed);
        _playerView = new EntityView(_animator);
    }

    private void OnEnable()
    {
        EventBus.Instance.OnGameEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnGameEvent -= HandleEvent;
    }

    private void Update()
    {
        if (_isPaused)
        {
            _moveInput = Vector2.zero;
            return;
        }
            

        _moveInput = _input.Player.Move.ReadValue<Vector2>();
        _playerView.SetMoveDirection(_moveInput);
        _playerView.SetMoving(_moveInput != Vector2.zero);
    }

    private void FixedUpdate()
    {       
        _movement.Move(_moveInput);
    }

    private void HandleEvent(IEvent @event)
    {
        if (@event is GamePausedEvent pause)
            _isPaused = pause.IsPaused;
    }
}
