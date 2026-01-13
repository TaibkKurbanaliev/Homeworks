using UnityEngine;

public class AIInputService : IInputService
{
    private Transform _target;
    private Transform _current;

    public AIInputService(Transform target, Transform current)
    {
        _target = target;
        _current = current;
    }

    public Vector2 GetMoveInput()
    {
        if (_target == null)
            return Vector2.zero;

        var dir = _target.position - _current.position;
        dir.y = 0f;
        dir.Normalize();

        return new Vector2(dir.x, dir.z);
    }

    public bool IsActionPressed()
    {
        throw new System.NotImplementedException();
    }
}
