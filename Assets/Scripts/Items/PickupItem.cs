using Mirror;
using UnityEngine;

public abstract class PickupItem : NetworkBehaviour
{
    public void Pickup()
    {
        NetworkServer.UnSpawn(gameObject);
    }
}
