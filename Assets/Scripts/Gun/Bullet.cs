using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _lifeTime = 1f;
    
    private Rigidbody _rb;
    private float _damage;
    private WaitForSeconds _lifeTimeWaiter;
    private CancellationTokenSource _cts;

    public void Init(float damage)
    {        
        _damage = damage;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //_cts = new CancellationTokenSource();
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _lifeTimeWaiter = new WaitForSeconds(_lifeTime);
        StartCoroutine(StartDestroy());
    }

    private void OnDisable()
    {
        //_cts.Cancel();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage, collision.contacts[0].normal);
            gameObject.SetActive(false);
        }
    }

    public void Shoot()
    {
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
        _ = StartDestroy();
    }

    public IEnumerator StartDestroy()
    {
        yield return _lifeTimeWaiter;
        gameObject.SetActive(false);
    }

    /*private async Task StartDestroy()
    {
        await Task.Delay((int)(_lifeTime * 1000), _cts.Token);
        gameObject.SetActive(false);
    }*/
}
