using Mirror;
using UnityEngine;

public abstract class PickupItem : NetworkBehaviour
{
    [SerializeField] private Collider _trigger;
    [SerializeField] private Animator _animator;

    [ClientRpc]
    public void RpcPickup()
    {
        _trigger.enabled = false;
        _animator.enabled = false;
        gameObject.SetActive(false);
    }

    public abstract void Use(Player user);
}
