using Cysharp.Threading.Tasks;
using Mirror;
using System;
using UnityEngine;

public class Grenade : PickupItem
{
    [SerializeField] private GrenadeConfig _cfg;
    [SerializeField] private LayerMask _target;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _explosionEffect;

    [Server]
    public override void Use(Player player)
    {
        StartDestroyTimer().Forget();
        transform.position = player.transform.position + Vector3.up * (player.CharacterController.height / 2) + (transform.forward * (player.CharacterController.radius + 0.3f));

        _rigidbody.useGravity = true;

        _rigidbody.linearVelocity = player.transform.forward * _cfg.ThrowForce;
        RpcThrow();
    }

    private async UniTask StartDestroyTimer()
    {
        await UniTask.WaitForSeconds(_cfg.TimeToExplosion);
        var overlaps = Physics.OverlapSphere(transform.position, _cfg.ExplosionRadius, _target);

        foreach (var collision in overlaps)
        {
            if (collision.TryGetComponent(out HealthComponent target))
            {
                target.TakeDamage(_cfg.Damage);
            }
        }

        RpcShowExplosion();

        await UniTask.WaitForSeconds(_cfg.TimeToDestroy);
        NetworkServer.Destroy(gameObject);
    }

    [ClientRpc]
    private void RpcThrow()
    {
        gameObject.SetActive(true);
    }

    [ClientRpc]
    private void RpcShowExplosion()
    {
        _explosionEffect.Play();
    }
}
