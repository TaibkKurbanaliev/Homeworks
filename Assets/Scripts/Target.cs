using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public enum LifeTimeBehaviour { Scale, Rotate }

[RequireComponent(typeof(Animator), typeof(Collider), typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    public event Action Hitted;

    private const string DestroyAnim = "Destroy";

    [SerializeField] private float _timeToDestroy;
    [SerializeField] private float _rotationDuration;
    [SerializeField] private float _scaleDuration;
    [SerializeField] private float _maxScale;

    private Material _material;
    private Animator _animator;
    private Collider _collider;
    private Rigidbody _rigidbody;

    private bool _isUpdated = false;
    private float _rotationAngle = 360f;
    private LifeTimeBehaviour _lifeTimeBehaviour;

    public void Init(LifeTimeBehaviour lifeTimeBehaviour)
    {
        _lifeTimeBehaviour = lifeTimeBehaviour;
        _material = GetComponent<Material>();
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Debug.Log("Target created");
        StartCoroutine(DestroyObject());
        SetBehaviour();
    }

    private void SetBehaviour()
    {
        switch (_lifeTimeBehaviour)
        {
            case LifeTimeBehaviour.Scale:
                transform.DOScale(_maxScale, _scaleDuration).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
                break;
            case LifeTimeBehaviour.Rotate:
                transform
                    .DORotate(new Vector3(0f, _rotationAngle, 0f), _rotationDuration, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1)
                    .SetLink(gameObject);
                break;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Hitted?.Invoke();
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
        _animator.Play(DestroyAnim);
    }

    private void Update()
    {
        if (!_isUpdated)
        {
            Debug.Log("Target still alive");
            _isUpdated = !_isUpdated;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Target destroyed");
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(gameObject);
    }

    public void HitDestroy()
    {
        Destroy(gameObject);
    }
}
