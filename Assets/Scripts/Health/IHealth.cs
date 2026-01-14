using System;
using UnityEngine;

public interface IHealth
{
    event Action Died;

    void ReduceHealth(float value);
    void RestoreHealth(float value);
    bool IsDied();
}
