using UnityEngine;

public class EntityView 
{
    private const string DIR_X = "DirX";
    private const string DIR_Y = "DirY";
    private const string IS_MOVING = "IsMoving";

    private Animator _animator;

    public EntityView(Animator animator)
    {
        _animator = animator;
    }

    public void SetMoving(bool isMoving)
    {
        _animator.SetBool(IS_MOVING, isMoving);
    }

    public void SetMoveDirection(Vector2 movDir)
    {
        _animator.SetFloat(DIR_X, movDir.x);
        _animator.SetFloat(DIR_Y, movDir.y);
    }
}
