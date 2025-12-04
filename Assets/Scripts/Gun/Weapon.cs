using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _shootDirection;
    [SerializeField] private WeaponConfig _config;
    
    private float _delay;
    private bool _canFire = true;

    private void Awake()
    {
        _delay = 1000f / _config.BulletsPerSecond;
    }

    public void Shoot()
    {
        if (!_canFire)
            return;

        _canFire = false;

        var bullet = Instantiate(_bullet, _spawnPoint.position, _spawnPoint.rotation);
        bullet.transform.rotation = new Quaternion(0f, bullet.transform.rotation.y, 0f, bullet.transform.rotation.w);
        bullet.Init(_config.Damage);
        _ = AwaitShootDelay();
    }

    private async Task AwaitShootDelay()
    {
        await Task.Delay((int)_delay);
        _canFire = true;
    }
}
