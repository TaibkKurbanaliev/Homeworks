using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour, IPauseView
{
    public event Action ResumeClicked;
    public event Action MenuClicked;

    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _menuButton;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        _menuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
        _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnMenuButtonClicked()
    {
        MenuClicked?.Invoke();
    }

    private void OnResumeButtonClicked()
    {
        ResumeClicked?.Invoke();
    }
}
