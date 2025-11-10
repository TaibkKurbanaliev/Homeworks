using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    public event Action Fired;

    private const string FireAnimTrigger = "Fire";

    [SerializeField] private Crosshair _crosshair;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private AudioClip _fireSound;

    private AudioSource _audioSource;
    private Transform _transform;
    private GunConfig _gunCfg;
    private Animator _animator;

    private bool _canFire = true;
    private float _reloadTime;

    public void Init(GunConfig gunCfg)
    {
        _gunCfg = gunCfg;
        _reloadTime = PlayerPrefs.HasKey(nameof(_reloadTime)) ? PlayerPrefs.GetFloat(nameof(_reloadTime)) : _gunCfg.RealoadTime;
        _transform = transform;
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        Vector3 direction = _crosshair.WorldPosition() - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(-direction);
        transform.rotation = targetRotation;
    }

    public void Fire()
    {
        if (_canFire)
        {
            var projectile = Instantiate(_bullet, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            projectile.Init(_gunCfg.ProjectileSpeed);
            _canFire = false;
            StartCoroutine(StartReloading());
            Fired?.Invoke();
            _animator.SetTrigger(FireAnimTrigger);
            _audioSource.PlayOneShot(_fireSound);
        }
        else
        {
            Debug.LogWarning("Reloading!!!");
        }
    }

    private IEnumerator StartReloading()
    {
        yield return new WaitForSeconds(_reloadTime);
        _canFire = true;
    }
}
