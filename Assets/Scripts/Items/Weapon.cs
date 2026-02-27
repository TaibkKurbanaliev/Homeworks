using Cysharp.Threading.Tasks;
using Mirror;
using UnityEngine;

public class Weapon : NetworkBehaviour
{
    [SerializeField] private WeaponConfig _config;
    [SerializeField] private AudioClip _fireSound;
    [SerializeField] private ParticleSystem _fireVFX;
    [SerializeField] private LayerMask _playerMask;
    [SyncVar] private bool _canFire = true;

    private AudioSource _audioSource;

    private int _maxBullerts;
    private int _currentBullets;


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

        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue))
        {
            Debug.Log("Popal");
            if ((_playerMask.value & (1 << hitInfo.collider.gameObject.layer)) != 0)
            {
                Debug.Log("popal v igroka");
                var enemy = hitInfo.transform.GetComponent<HealthComponent>();
                enemy.TakeDamage(_config.Damage);
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(Camera.main.transform.position + Camera.main.transform.forward * 0.4f, Camera.main.transform.position + Camera.main.transform.forward * 100, Color.red);
    }

    private async UniTask StartCooldown()
    {
        await UniTask.WaitForSeconds(1 / _config.FireRate);
        _canFire = true;
    }
}
