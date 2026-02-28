using System;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private readonly int _animDirXHash = Animator.StringToHash("DirX");
    private readonly int _animDirYHash = Animator.StringToHash("DirY");
    private readonly int _animIsDiedHash = Animator.StringToHash("IsDied");

    [SerializeField] private Renderer _renderer;
    [SerializeField] private TMP_Text _name;

    [SerializeField] private GameObject _fpView;
    [SerializeField] private GameObject _tpView;

    [SerializeField] private Animator _fpAnimator;
    [SerializeField] private Animator _tpAnimator;


    private InstanceInfo _info;

    public void Init(InstanceInfo info)
    {
        _name.text = info.Name;
        _renderer.material.color = info.Color;

        _info = info;
        _info.NameChanged += OnNameChanged;
        _info.ColorChanged += OnColorChanged;
    }

    public void SetFPView()
    {
        _fpView.SetActive(true);
        _tpView.SetActive(false);
    }

    public void SetAnimDirection(Vector2 input)
    {
        _tpAnimator.SetFloat(_animDirXHash, input.x);
        _tpAnimator.SetFloat(_animDirYHash, input.y);
    }

    public void SetDeath()
    {
        _tpAnimator.SetBool(_animIsDiedHash, true);
        _fpView.SetActive(false);
        _tpView.SetActive(true);
    }

    public void SetAlive() => _tpAnimator.SetBool(_animIsDiedHash, false);
    private void OnColorChanged(Color color) => _renderer.material.color = color;
    private void OnNameChanged(string name) => _name.text = name;
}
