using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayHUD : MonoBehaviour, IGameplayHUD
{
    public event Action PauseClicked;

    [SerializeField] private Button _pauseButton;
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _points;

    private IHealth _playerHealth;

    public void Construct(IHealth health)
    {
        _playerHealth = health;
    }

    private void OnEnable()
    {
        _playerHealth.HealthChanged += OnHealthChanged;
        _pauseButton.onClick.AddListener(OnPauseClicked);
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= OnHealthChanged;
        _pauseButton.onClick.RemoveListener(OnPauseClicked);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnHealthChanged(float health)
    {
        _health.text = health.ToString();
    }

    private void OnPauseClicked()
    {
        PauseClicked?.Invoke();
    }
}
