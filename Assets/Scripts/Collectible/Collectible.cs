using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public event Action<Collectible> OnCollected;
    [field: SerializeField] public CollectibleConfig Config { get; private set; }

    public void Construct(CollectibleConfig config)
    {
        Config = config;
    }

    private void Update()
    {
        transform.Rotate(transform.up, Config.RotationSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        OnCollected?.Invoke(this);
    }
}
