using System.Collections;
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
    private float _delay;
    private bool _canFire = true;
    private bool _isReloading = false;
    private CancellationTokenSource _cts;

    [field: SerializeField] public WeaponConfig Config { get; private set; }
    public int CurrentNumberOfBullets { get; private set; }

    private void Start()
    {
        _delay = 1000f / Config.BulletsPerSecond;
        CurrentNumberOfBullets = Config.BulletStore;
        _audio = GetComponent<AudioSource>();
        EventBus.Instance.TriggerEvent(new BulletsAmountChangeEvent(Config.BulletStore));
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
        var bullet = Instantiate(_bullet, _spawnPoint.position, _spawnPoint.rotation);
        bullet.transform.rotation = new Quaternion(0f, 
                                                   bullet.transform.rotation.y + Random.Range(-Config.Spread, Config.Spread), 
                                                   0f, 
                                                   bullet.transform.rotation.w);
        bullet.Init(Config.Damage);
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
