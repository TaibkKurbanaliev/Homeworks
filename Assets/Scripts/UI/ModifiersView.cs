using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ModifiersView : MonoBehaviour, IModifiersView
{
    public event Action<int> ModifierDeleted;
    public event Action<ModifierType> ModifierAdded;

    [SerializeField] private Transform _container;
    [SerializeField] private ModifierView _prefab;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _removeButton;
    [SerializeField] private ModifierConfig _config;

    private List<ModifierView> _modifiers = new();
    private List<ModifierType> _modifierTypes;

    public void Init()
    {
        _modifierTypes = Enum.GetValues(typeof(ModifierType)).Cast<ModifierType>().ToList();

        foreach (var type in _config.StartModifiers)
        {
            var modifier = Instantiate(_prefab, _container);
            modifier.Init(type);
            _modifiers.Add(modifier);
        }
    }

    private void OnEnable()
    {
        _addButton.onClick.AddListener(AddRandomModifier);
        _removeButton.onClick.AddListener(RemoveLastModifier);
    }

    private void OnDisable()
    {
        _addButton.onClick.RemoveListener(AddRandomModifier);
        _removeButton.onClick.RemoveListener(RemoveLastModifier);
    }

    public void AddRandomModifier()
    {
        var modifier = Instantiate(_prefab, _container);
        var modifierType = _modifierTypes[Random.Range(0, _modifierTypes.Count)];

        modifier.Init(modifierType);
        _modifiers.Add(modifier);
        ModifierAdded?.Invoke(modifierType);
    }

    public void RemoveLastModifier()
    {
        var modifier = _modifiers.LastOrDefault();

        if (modifier != null)
        {
            var index = _modifiers.IndexOf(modifier);
            _modifiers.RemoveAt(index);
            Destroy(modifier.gameObject);
            ModifierDeleted?.Invoke(index);
        }
            
    }
}
