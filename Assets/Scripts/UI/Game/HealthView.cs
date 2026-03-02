using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Slider _health;
    private EventBinding<HealthChangedEvent> _healthChangedBinding;

    private void Awake()
    {
        _healthChangedBinding = new EventBinding<HealthChangedEvent>(OnHealthChanged);
        EventBus<HealthChangedEvent>.Register(_healthChangedBinding);
    }

    private void OnDestroy()
    {
        EventBus<HealthChangedEvent>.Deregister(_healthChangedBinding);
    }

    private void OnHealthChanged(HealthChangedEvent @event)
    {
        _health.value = @event.Value;
    }
}
