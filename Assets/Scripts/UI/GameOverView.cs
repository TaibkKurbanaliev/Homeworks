using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour, IGameOverView
{
    public event Action RestartClicked;
    public event Action MenuClicked;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _menuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
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

    private void OnRestartButtonClicked()
    {
        RestartClicked?.Invoke();
    }
}
