using UnityEngine;

public class FakeInputService : IInputService
{
    private Vector2 _testDirection;
    private bool _enabled = true;

    public FakeInputService(Vector2 testDirection)
    {
        _testDirection = testDirection;
    }

    public Vector2 GetMoveInput()
    {
        if (_enabled) 
            return _testDirection;

        return Vector2.zero;
    }

    public bool IsActionPressed()
    {
        return false;
    }

    public void SetActive(bool isEnabled)
    {
        _enabled = isEnabled;
    }
}
