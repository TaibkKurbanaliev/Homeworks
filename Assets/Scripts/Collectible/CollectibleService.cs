using System;
using UnityEngine;

public class CollectibleService : ICollectibleService
{
    public event Action<int> Collected;

    private int _count;

    public void Collect()
    {
        Collected?.Invoke(++_count);
    }
}
