using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IMainMenuView
{
    public event Action StartClicked;
    public event Action ExitClicked;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnExitButtonClicked()
    {
        ExitClicked?.Invoke();
    }

    private void OnPlayButtonClicked()
    {
        StartClicked?.Invoke();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
