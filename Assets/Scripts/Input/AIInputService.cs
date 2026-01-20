using UnityEngine;

public class AIInputService : IInputService
{
    private Vector3 _target;
    private Transform _current;
    private bool _enabled = true;

    public AIInputService(Vector3 target, Transform current)
    {
        _target = target;
        _current = current;
    }

    public Vector2 GetMoveInput()
    {
        if (_target == null || !_enabled)
            return Vector2.zero;

        var dir = _target - _current.position;
        dir.y = 0f;
        dir.Normalize();

        return new Vector2(dir.x, dir.z);
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
