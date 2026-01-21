using System;
using UnityEngine;

public interface IModifiersView
{
    event Action<int> ModifierDeleted;
    event Action<ModifierType> ModifierAdded;
    void AddRandomModifier();
    void RemoveLastModifier();
    void Init();
}
