using UnityEngine;

public class Player : MonoBehaviour
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
}
