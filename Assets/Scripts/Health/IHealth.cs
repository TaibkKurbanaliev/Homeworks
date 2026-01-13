using UnityEngine;

public interface IHealth
{
    void ReduceHealth(float value);
    void RestoreHealth(float value);
    bool IsDied();
}
