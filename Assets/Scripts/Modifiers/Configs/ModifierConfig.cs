using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifierConfig", menuName = "Scriptable Objects/ModifierConfig")]
public class ModifierConfig : ScriptableObject
{

    [SerializeField] private ServicesSOAP _services;

    [field: SerializeField] public List<ModifierType> StartModifiers { get; private set; }
    [field: SerializeField] public SpeedModifierConfig SpeedModifierConfig { get; private set; }
    [field: SerializeField] public RegenModifierConfig RegenModifierConfig { get; private set; }
    [field: SerializeField] public NoInputModifierConfig NoInputModifierConfig { get; private set; }

    private List<IGameModifier> _modifiers = new();

    public IEnumerable<IGameModifier> Modifiers => _modifiers;

    public void Init()
    {
        _modifiers.Clear();

        foreach (var type in StartModifiers)
            _modifiers.Add(GetModifier(type));
    }

    public IGameModifier GetModifier(ModifierType type)
    {
        switch (type)
        {
            case ModifierType.Speed:
                return new SpeedModifier(_services.Movement, SpeedModifierConfig);
            case ModifierType.Regen:
                return new RegenModifier(_services.Health, RegenModifierConfig);
            case ModifierType.Input:
                return new NoInputModifier(_services, NoInputModifierConfig);
            default:
                throw new NotImplementedException(type.ToString());
        }
    }
}

public enum ModifierType
{
    Speed,
    Regen,
    Input
}