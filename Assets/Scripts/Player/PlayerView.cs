using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour 
{
    [SerializeField] private Transform _forwardTarget;
    private readonly int _dirXParameterHash = Animator.StringToHash("DirX");
    private readonly int _dirYParameterHash = Animator.StringToHash("DirY");
    private readonly int _deathParameterHash = Animator.StringToHash("Death");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 inputDir)
    {
        Vector3 viewDir = transform.InverseTransformDirection(new Vector3(inputDir.x, 0f, inputDir.y));
        _animator.SetFloat(_dirXParameterHash, viewDir.x);
        _animator.SetFloat(_dirYParameterHash, viewDir.z);
    }

    public void LookAtMouse()
    {
        transform.rotation = _forwardTarget.rotation;
    }

    public void PlayDeathAnim()
    {
        _animator.SetTrigger(_deathParameterHash);
    }
}
