using Cysharp.Threading.Tasks;
using Mirror;
using UnityEngine;

public class Weapon : NetworkBehaviour
{
    [SerializeField] private WeaponConfig _config;
    [SerializeField] private LayerMask _playerMask;
    [SyncVar] private bool _canFire = true;

    private int _maxBullerts;
    private int _currentBullets;


    private void Awake()
    {
        _maxBullerts = _config.NumberOfBullets;
        _currentBullets = _maxBullerts;
    }

    [Client]
    public void Shoot()
    {
        if (!_canFire)
            return;

        CmdShoot();
    }

    [Command]
    private void CmdShoot()
    {
        if (!_canFire)
            return;

        var camera = Camera.main;
        var ray = new Ray(camera.transform.position, camera.transform.forward);
        StartCooldown().Forget();
        _canFire = false;

        Debug.Log("Shoot"); 

        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue))
        {
            if ((_playerMask.value & (1 << hitInfo.collider.gameObject.layer)) != 0)
            {
                Debug.Log("Popal v player");
                return;
            }

            Debug.Log("Popal v stenu");
        }
    }

    private async UniTask StartCooldown()
    {
        await UniTask.WaitForSeconds(1 / _config.FireRate);
        _canFire = true;
    }
}
