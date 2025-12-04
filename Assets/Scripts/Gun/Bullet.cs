using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _lifeTime = 1f;
    
    private Rigidbody _rb;
    private float _damage;

    public void Init(float damage)
    {
        _rb = GetComponent<Rigidbody>();
        _damage = damage;
        Shoot();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage, collision.contacts[0].normal);
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
        _ = StartDestroy();
    }

    private async Task StartDestroy()
    {
        await Task.Delay((int)(_lifeTime * 1000));
        Destroy(gameObject);
    }
}
