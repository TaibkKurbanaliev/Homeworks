using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InstanceInfoController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private ColorPalette _colors;
    [SerializeField] private Button _readyButton;

    private EventBinding<InstanceConnectedEvent> _instanceInfoBinding;
    private InstanceInfo _info;

    private void Awake()
    {
        _nameInput.onValueChanged.AddListener(OnNameChanged);
        _colors.ColorChanged += OnColorChanged;
        _readyButton.onClick.AddListener(OnReadyClicked);
        _instanceInfoBinding = new EventBinding<InstanceConnectedEvent>(OnInstanceConnected);
        EventBus<InstanceConnectedEvent>.Register(_instanceInfoBinding);
    }

    private void OnInstanceConnected(InstanceConnectedEvent @event) 
    {
        _info = @event.Info;
    }

    private void OnDestroy()
    {
        EventBus<InstanceConnectedEvent>.Deregister(_instanceInfoBinding);
        _nameInput.onValueChanged.RemoveListener(OnNameChanged);
        _colors.ColorChanged -= OnColorChanged;
        _readyButton.onClick.RemoveListener(OnReadyClicked);
    }

    private void OnReadyClicked()
    {
        _info.SetReady();
    }

    private void OnColorChanged(Color color)
    {
        _info.SetColor(color);
    }

    private void OnNameChanged(string name)
    {
        _info.SetName(name);
    }
}