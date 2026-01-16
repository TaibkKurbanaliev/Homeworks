using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayHUD : MonoBehaviour, IGameplayHUD
{
    public event Action PauseClicked;
    public event Action<InputType> InputTypeChanged;

    [SerializeField] private Button _pauseButton;
    [SerializeField] private TMP_Dropdown _inputTypes;
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _points;

    private IHealth _playerHealth;
    private ICollectibleService _collectibleService;

    public void Construct(IHealth health, ICollectibleService collectible)
    {
        _collectibleService = collectible;
        _playerHealth = health;
        _inputTypes.AddOptions(Enum.GetNames(typeof(InputType)).ToList());
        _inputTypes.onValueChanged.AddListener(OnInputTypeChanged);
    }

    private void OnEnable()
    {
        _playerHealth.HealthChanged += OnHealthChanged;
        _collectibleService.Collected += OnCollected;
        _pauseButton.onClick.AddListener(OnPauseClicked);
        _points.text = 0f.ToString();
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= OnHealthChanged;
        _collectibleService.Collected -= OnCollected; 
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

    private void OnCollected(int count)
    {
        _points.text = count.ToString();
    }

    public void OnInputTypeChanged(int value)
    {
        InputTypeChanged?.Invoke((InputType)value);
    }
}
