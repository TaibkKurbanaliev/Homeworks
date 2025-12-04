using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _deathText;
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _reloadButton;

    private int _numberOfDiedEnemies;

    private void OnEnable()
    {
        _reloadButton.onClick.AddListener(OnReloadButtonClicked);
        EventBus.Instance.AddListener<PlayerHealthChangeEvent>(OnHealthChanged);
        EventBus.Instance.AddListener<EnemyDiedEvent>(OnEnemyDied);
        EventBus.Instance.AddListener<PlayerDeathEvent>(OnPlayerDied);
    }

    private void OnDisable()
    {
        _reloadButton.onClick.RemoveListener(OnReloadButtonClicked);
        EventBus.Instance.RemoveListener<PlayerHealthChangeEvent>(OnHealthChanged);
        EventBus.Instance.RemoveListener<EnemyDiedEvent>(OnEnemyDied);
        EventBus.Instance.RemoveListener<PlayerDeathEvent>(OnPlayerDied);
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

    private void OnPlayerDied(PlayerDeathEvent _)
    {
        _deathText.gameObject.SetActive(true); 
    }
}
