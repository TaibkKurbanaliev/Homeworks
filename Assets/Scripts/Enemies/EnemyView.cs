using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyView : MonoBehaviour
{
    public event Action OnAttackAnimEnded;

    [SerializeField] private ParticleSystem _bloodParticle;

    private readonly int _speedParameterHash = Animator.StringToHash("Speed");
    private readonly int _attackParameterHash = Animator.StringToHash("Attack");
    private readonly int _dieParameterHash = Animator.StringToHash("Death");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(_speedParameterHash, speed);
    }

    public void Attack()
    {
        _animator.SetTrigger(_attackParameterHash);
    }

    public void EndOfAttack(AnimationEvent evt)
    {
        OnAttackAnimEnded?.Invoke();
    }

    public void PlayDieAnim()
    {
        _animator.SetTrigger(_dieParameterHash);
    }

    public void PlayHitEffect(Vector3 hitNormal)
    {
        _bloodParticle.transform.rotation = Quaternion.LookRotation(hitNormal);
        _bloodParticle.Play();
    }
}
