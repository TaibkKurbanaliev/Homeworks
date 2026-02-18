using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceInfoController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private ColorPalette _colors;
    [SerializeField] private Button _readyButton;

    private void Awake()
    {
        _nameInput.onValueChanged.AddListener(OnNameChanged);
        _colors.ColorChanged += OnColorChanged;
        _readyButton.onClick.AddListener(OnReadyClicked);
    }

    private void OnDestroy()
    {
        _nameInput.onValueChanged.RemoveListener(OnNameChanged);
        _colors.ColorChanged -= OnColorChanged;
        _readyButton.onClick.RemoveListener(OnReadyClicked);
    }

    private void OnReadyClicked()
    {
        var info = ClientInstance.ReturnClientInstance().GetComponent<InstanceInfo>();
        info.SetReady();
    }

    private void OnColorChanged(Color color)
    {
        var info = ClientInstance.ReturnClientInstance().GetComponent<InstanceInfo>();
        info.SetColor(color);
    }

    private void OnNameChanged(string name)
    {
        var info = ClientInstance.ReturnClientInstance().GetComponent<InstanceInfo>();
        info.SetName(name);
    }
}