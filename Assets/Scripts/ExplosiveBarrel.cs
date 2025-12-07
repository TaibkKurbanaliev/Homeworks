using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private AudioClip _explosion;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private LayerMask _bulletMask;
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;
    [SerializeField] private float _force;


    private AudioSource _audioSource;
    private MeshRenderer _render;
    private Collider _collider;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _render = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((_bulletMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            Explode();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void Explode()
    {
        Collider[] overlapColliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var overlap in overlapColliders)
        {
            if (overlap.attachedRigidbody)
            {
                overlap.attachedRigidbody.AddExplosionForce(_force, transform.position, _radius);

                if (overlap.TryGetComponent(out IDamagable damagable))
                {
                    var damagePercent = 1 - ((Vector3.Distance(overlap.gameObject.transform.position, transform.position)) / _radius);
                    var radialDamage = _damage * damagePercent;
                    damagable.TakeDamage(radialDamage, Vector3.zero);
                }
            }
        }

        _ = WaitSoundEffect();
    }

    private async Task WaitSoundEffect()
    {
        _particle.Play();
        _audioSource.PlayOneShot(_explosion);
        _collider.enabled = false;
        _render.enabled = false;
        await Task.Delay((int)(_explosion.length * 1000f));
        Destroy(gameObject);
    }
}
