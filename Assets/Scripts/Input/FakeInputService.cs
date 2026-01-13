using UnityEngine;

public class FakeInputService : IInputService
{
    private Vector2 _testDirection;

    public FakeInputService(Vector2 testDirection)
    {
        _testDirection = testDirection;
    }

    public Vector2 GetMoveInput()
    {
        return _testDirection;
    }

    public bool IsActionPressed()
    {
        return false;
    }
}
