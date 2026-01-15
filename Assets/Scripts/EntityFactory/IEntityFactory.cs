using UnityEngine;

public interface IEntityFactory<T>
{
    T Create(T prefab, Vector3 pos);
}
