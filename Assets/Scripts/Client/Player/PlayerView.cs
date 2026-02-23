using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TMP_Text _name;

    private InstanceInfo _info;

    public void Init(InstanceInfo info)
    {
        _name.text = info.Name;
        _renderer.material.color = info.Color;

        _info = info;
        _info.NameChanged += OnNameChanged;
        _info.ColorChanged += OnColorChanged;
    }

    private void OnColorChanged(Color color)
    {
        _renderer.material.color = color;
    }

    private void OnNameChanged(string name)
    {
        _name.text = name;
    }
}
