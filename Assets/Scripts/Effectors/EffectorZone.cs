using Mirror;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EffectorZone : NetworkBehaviour
{
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private List<Effect> _effects;

    protected override void OnValidate()
    {
        if (_collider == null)
            _collider = GetComponent<BoxCollider>();
    }

    [Server]
    private void OnTriggerEnter(Collider other)
    {
        if (!((_playerMask.value & (1 << other.gameObject.layer)) != 0))
            return;

        foreach (var effect in _effects)
        {
            if (other.TryGetComponent(out Player player))
                effect.ApplyEffect(player);
        }
    }

    [Server]
    private void OnTriggerExit(Collider other)
    {
        if (!((_playerMask.value & (1 << other.gameObject.layer)) != 0))
            return;

        foreach (var effect in _effects)
        {
            if (other.TryGetComponent(out Player player))
                effect.StopEffect(player);
        }
    }
}
