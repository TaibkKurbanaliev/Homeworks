using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _deathText;
    [SerializeField] private TMP_Text _currentNumberOfBullets;
    [SerializeField] private TMP_Text _newWaveText;
    [SerializeField] private TMP_Text _winText;

    [SerializeField] private Image _weaponIcon;
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _reloadButton;

    private int _numberOfDiedEnemies;

    private void OnEnable()
    {
        _reloadButton.onClick.AddListener(OnReloadButtonClicked);
        EventBus.Instance.AddListener<PlayerHealthChangeEvent>(OnHealthChanged);
        EventBus.Instance.AddListener<EnemyDiedEvent>(OnEnemyDied);
        EventBus.Instance.AddListener<LoseEvent>(OnLose);
        EventBus.Instance.AddListener<WinEvent>(OnWin);
        EventBus.Instance.AddListener<BulletsAmountChangeEvent>(OnWeaponShootEvent);
        EventBus.Instance.AddListener<SwapWeaponEvent>(OnWeaponSwapped);
        EventBus.Instance.AddListener<NewWaveEvent>(OnNewWave);
    }

    private void OnDisable()
    {
        _reloadButton.onClick.RemoveListener(OnReloadButtonClicked);
        EventBus.Instance.RemoveListener<PlayerHealthChangeEvent>(OnHealthChanged);
        EventBus.Instance.RemoveListener<EnemyDiedEvent>(OnEnemyDied);
        EventBus.Instance.RemoveListener<LoseEvent>(OnLose);
        EventBus.Instance.RemoveListener<WinEvent>(OnWin);
        EventBus.Instance.RemoveListener<BulletsAmountChangeEvent>(OnWeaponShootEvent);
        EventBus.Instance.RemoveListener<SwapWeaponEvent>(OnWeaponSwapped);
        EventBus.Instance.RemoveListener<NewWaveEvent>(OnNewWave);
    }

    private void OnReloadButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnHealthChanged(PlayerHealthChangeEvent @event)
    {
        _slider.value = @event.Health;
    }

    private void OnEnemyDied(EnemyDiedEvent @event)
    {
        _score.text = (++_numberOfDiedEnemies).ToString();
    }

    private void OnLose(LoseEvent _)
    {
        _deathText.gameObject.SetActive(true); 
    }

    private void OnWeaponShootEvent(BulletsAmountChangeEvent @event)
    {
        _currentNumberOfBullets.text = @event.NumberOfBullets.ToString();
    }

    private void OnWeaponSwapped(SwapWeaponEvent @event)
    {
        _weaponIcon.sprite = @event.Icon;
        _currentNumberOfBullets.text = @event.CurrentBullets.ToString();
    }
    private void OnNewWave(NewWaveEvent @event)
    {
        _ = ShowNewWaveText();
    }


    private void OnWin(WinEvent @event)
    {
        _winText.gameObject.SetActive(true);
    }


    private async Task ShowNewWaveText()
    {
        _newWaveText.gameObject.SetActive(true);
        await Task.Delay(1000);
        _newWaveText.gameObject.SetActive(false);
    }
}
