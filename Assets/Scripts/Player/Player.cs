using UnityEngine;

public class Player : MonoBehaviour
{
    private IInputService _input;
    private IMovementService _movement;

    public void Construct(IInputService inputService, IMovementService movement)
    {
        _input = inputService;
        _movement = movement;
    }

    private void FixedUpdate()
    {
        _movement.Move(_input.GetMoveInput());
    }
}
