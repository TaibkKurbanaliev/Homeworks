using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPalette : MonoBehaviour
{
    public event Action<Color> ColorChanged;

    [SerializeField] private List<Toggle> _toggles;

    private void Start()
    {
        foreach (var toggle in _toggles)
        {
            toggle.onValueChanged.AddListener(isOn =>
            {
                if (isOn)
                    ColorChanged?.Invoke(toggle.GetComponentInChildren<Image>().color);
            });
        }
    }
}
