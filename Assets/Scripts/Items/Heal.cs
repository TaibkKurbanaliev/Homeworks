using Mirror;
using UnityEngine;

public class Heal : PickupItem
{
    [SerializeField] private float _amount = 10f;

    [Server]
    public override void Use(Player player)
    {
        player.Health.Heal(_amount);
        NetworkServer.Destroy(gameObject);
    }
}
