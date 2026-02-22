using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TMP_Text _name;

    public void Init(InstanceInfo info)
    {
        _name.text = info.Name;
        _renderer.material.color = info.Color;
    }
}
