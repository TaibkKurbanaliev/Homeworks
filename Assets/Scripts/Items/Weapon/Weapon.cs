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
    [SyncVar(hook = nameof(OnBulletsChanged))] private int _currentBullets;
    [SyncVar] private int _maxBullets;
    [SyncVar] private bool _canFire = true;

    private AudioSource _audioSource;
    private Player _owner;


    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        EventBus<BulletsChangedEvent>.Raise(new BulletsChangedEvent { Count = _currentBullets });
    }

    public void Init(Player player)
    {
        _owner = player;
    }

    private void Awake()
    {
        CmdInitBullets();
        _audioSource = GetComponent<AudioSource>();
    }

    [Client]
    public void Reload()
    {
        CmdReload();
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
        if (!_canFire || _currentBullets <= 0)
            return;

        _currentBullets--;
        _canFire = false;

        var ray = new Ray(startPos, direction);
        StartCooldown().Forget();

        RpcShowTrace();

        var hits = Physics.RaycastAll(ray, float.MaxValue);

        foreach (var hit in hits)
        {
            if (hit.transform.root == _owner.transform)
                continue;

            if ((_playerMask.value & (1 << hit.collider.gameObject.layer)) != 0)
            {
                var enemy = hit.transform.GetComponent<HealthComponent>();

                if (enemy != null)
                {
                    enemy.TakeDamage(_config.Damage, _owner);
                    break;
                }
            }
        }
    }

    [Command]
    private void CmdReload()
    {
        StartReload().Forget();
    }

    [Command]
    private void CmdInitBullets()
    {
        _maxBullets = _config.NumberOfBullets;
        _currentBullets = _maxBullets;
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

        if (_currentBullets == 0)
            return;

        _canFire = true;
    }

    private async UniTask StartReload()
    {
        _canFire = false;

        await UniTask.WaitForSeconds(_config.ReloadTime);

        _currentBullets = _maxBullets;

        _canFire = true;
    }

    private void OnBulletsChanged(int prev, int next)
    {
        if (!isOwned)
            return;

        EventBus<BulletsChangedEvent>.Raise(new BulletsChangedEvent { Count = _currentBullets });
    }
}
