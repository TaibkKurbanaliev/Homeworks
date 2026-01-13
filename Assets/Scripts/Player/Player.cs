using UnityEngine;

public class Player : MonoBehaviour
{
    private IInputService _inputService;

    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void Update()
    {
        if (_inputService.GetMoveInput() != Vector2.zero)
            Debug.Log(_inputService.GetMoveInput());

        if (_inputService.WasActionPressed())
            Debug.Log(_inputService.WasActionPressed());
    }
}
