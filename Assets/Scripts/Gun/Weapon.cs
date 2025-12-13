using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _shootDirection;
    [SerializeField] private Transform _secondHand;
    [SerializeField] private Transform _secondHandGrabPoint;

    private AudioSource _audio;
    private CancellationTokenSource _cts;
    private List<Bullet> _bullets = new();
    private float _delay;
    private bool _canFire = true;
    private bool _isReloading = false;

    [field: SerializeField] public WeaponConfig Config { get; private set; }
    public int CurrentNumberOfBullets { get; private set; }

    private void Start()
    {
        _delay = 1000f / Config.BulletsPerSecond;
        CurrentNumberOfBullets = Config.BulletStore;
        _audio = GetComponent<AudioSource>();
        EventBus.Instance.TriggerEvent(new BulletsAmountChangeEvent(Config.BulletStore));

        for (int i = 0; i < Config.BulletStore; i++)
        {
            var bullet = Instantiate(_bullet, _spawnPoint.position, _spawnPoint.rotation);
            bullet.Init(Config.Damage);
            bullet.gameObject.SetActive(false);
            _bullets.Add(bullet);
        }
    }

    private void Update()
    {
        _secondHand.position = _secondHandGrabPoint.position;
    }

    private void OnEnable()
    {
        _cts = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        _audio.Stop();
        _cts.Cancel();
    }

    public void Shoot()
    {
        if (!_canFire || _isReloading)
            return;

        _canFire = false;

        _audio.PlayOneShot(Config.FireSound);
        var bullet = _bullets.FirstOrDefault(b => !b.gameObject.activeSelf);

        if (bullet == null)
        {
            bullet = Instantiate(_bullet, _spawnPoint.position, _spawnPoint.rotation);
            bullet.Init(Config.Damage);
            _bullets.Add(bullet);
        }

        bullet.transform.SetPositionAndRotation(_spawnPoint.position, _spawnPoint.rotation);
        bullet.transform.rotation = new Quaternion(0f,
                                                   bullet.transform.rotation.y + Random.Range(-Config.Spread, Config.Spread),
                                                   0f,
                                                   bullet.transform.rotation.w);
        bullet.gameObject.SetActive(true);
        bullet.Shoot();
        CurrentNumberOfBullets--;
        EventBus.Instance.TriggerEvent(new BulletsAmountChangeEvent(CurrentNumberOfBullets));
        
        if (CurrentNumberOfBullets == 0)
            return;

        _ = AwaitShootDelay();
    }
    public async Task Reload()
    {
        _cts = new CancellationTokenSource();
        _isReloading = true;
        _audio.PlayOneShot(Config.ReloadSound);

        await Task.Delay((int)(Config.ReloadTime * 1000f), _cts.Token);

        CurrentNumberOfBullets = Config.BulletStore;
        EventBus.Instance.TriggerEvent(new BulletsAmountChangeEvent(Config.BulletStore));
        _isReloading = false;
        _canFire = true;
    }

    private async Task AwaitShootDelay()
    {
        await Task.Delay((int)_delay);
        _canFire = true;
    }

}
