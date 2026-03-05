using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsController : NetworkBehaviour
{
    [SerializeField] private LayerMask _pickupItemLayer;
    private SyncList<Heal> _heals = new();
    private SyncList<Grenade> _grenades = new();

    private Player _player;
    private IInput _input;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void Init(IInput input)
    {
        _input = input;
        _input.GrenadeThrowed += OnGranadeThrowed;
        _input.Healed += OnHealed;  
    }

    public override void OnStartAuthority()
    {
        _heals.OnAdd += OnHealAdd;
        _heals.OnRemove += OnHealRemoved;
        _grenades.OnAdd += OnGranadeAdd;
        _grenades.OnRemove += OnGranadeRemoved;
    }

    public override void OnStopAuthority()
    {
        _heals.OnAdd -= OnHealAdd;
        _heals.OnRemove -= OnHealRemoved;
        _grenades.OnAdd -= OnGranadeAdd;
        _grenades.OnRemove -= OnGranadeRemoved;
        _input.GrenadeThrowed -= OnGranadeThrowed;
        _input.Healed -= OnHealed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_pickupItemLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            CmdPickup(other.gameObject);
        }
    }

    private void OnHealAdd(int index) => EventBus<ItemCountChangedEvent>.Raise(new ItemCountChangedEvent { Item = ItemType.Heal, Count = _heals.Count });
    private void OnGranadeAdd(int index) => EventBus<ItemCountChangedEvent>.Raise(new ItemCountChangedEvent { Item = ItemType.Grenade, Count = _grenades.Count });
    private void OnGranadeRemoved(int arg1, Grenade granade) => EventBus<ItemCountChangedEvent>.Raise(new ItemCountChangedEvent { Item = ItemType.Grenade, Count = _grenades.Count });
    private void OnHealRemoved(int arg1, Heal heal) => EventBus<ItemCountChangedEvent>.Raise(new ItemCountChangedEvent { Item = ItemType.Heal, Count = _heals.Count });


    private void OnGranadeThrowed()
    {
        if (!isOwned)
            return;

        if (_grenades.Count == 0)
            return;

        CmdThrowGrenade();
    }

    private void OnHealed()
    {
        if (!isOwned)
            return;

        if (_heals.Count == 0)
            return;

        CmdHeal();
    }

    [Command]
    private void CmdThrowGrenade()
    {
        var grenade = _grenades.First();
        grenade.Use(_player);
        grenade.gameObject.SetActive(true);
        _grenades.Remove(grenade);
    }

    [Command]
    private void CmdHeal()
    {
        var heal = _heals.First();
        heal.Use(_player);
        heal.gameObject.SetActive(true);
        _heals.Remove(heal);
    }

    [Command]
    private void CmdPickup(GameObject item)
    {
        var pickup = item.GetComponent<PickupItem>();
        pickup.RpcPickup();

        switch (pickup)
        {
            case Heal heal:
                _heals.Add(heal);
                return;
            case Grenade grenade:
                _grenades.Add(grenade);
                return;
        }
    }
}
