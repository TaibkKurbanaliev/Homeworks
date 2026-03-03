using Cysharp.Threading.Tasks;
using Mirror;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : NetworkBehaviour
{
    [SerializeField] private WeaponConfig _config;
    [SerializeField] private AudioClip _fireSound;
    [SerializeField] private ParticleSystem _fireVFX;
    [SerializeField] private BulletTrail _bullet;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Transform _muzzlePoint;
    [SyncVar] private bool _canFire = true;

    private AudioSource _audioSource;
    private Player _owner;

    private int _maxBullerts;
    private int _currentBullets;

    public void Init(Player player)
    {
        _owner = player;
    }

    private void Awake()
    {
        _maxBullerts = _config.NumberOfBullets;
        _currentBullets = _maxBullerts;
        _audioSource = GetComponent<AudioSource>();
    }

    [Client]
    public void Shoot()
    {
        if (!_canFire)
            return;

        var camera = Camera.main;
        _fireVFX.Play();
        _audioSource.Play();
        CmdShoot(camera.transform.position + camera.transform.forward * 0.5f, camera.transform.forward);
    }

    [Command]
    private void CmdShoot(Vector3 startPos, Vector3 direction)
    {
        if (!_canFire)
            return;

        var ray = new Ray(startPos, direction);
        StartCooldown().Forget();
        _canFire = false;

        RpcShowTrace();

        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue))
        {
            if ((_playerMask.value & (1 << hitInfo.collider.gameObject.layer)) != 0)
            {
                var enemy = hitInfo.transform.GetComponent<HealthComponent>();
                enemy.TakeDamage(_config.Damage, _owner);
            }
        }
    }

    [ClientRpc]
    private void RpcShowTrace()
    {
        if (isOwned)
            return;

        Instantiate(_bullet, _muzzlePoint.position, _muzzlePoint.transform.rotation);
    }

    private async UniTask StartCooldown()
    {
        await UniTask.WaitForSeconds(1 / _config.FireRate);
        _canFire = true;
    }
}
