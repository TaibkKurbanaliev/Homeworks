using UnityEngine;

public interface IInputService
{
    Vector2 GetMoveInput();
    bool WasActionPressed();
}
