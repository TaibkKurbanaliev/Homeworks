using System;
using UnityEngine;

public interface ICollectibleService
{
    event Action<int> Collected;
    void Collect();
}
