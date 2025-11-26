using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private Button _showAllButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _pauseButton;

    private GameStateService _gameStateService;

    public void Init(GameStateService service)
    {
        _gameStateService = service;
    }

    private void OnEnable()
    {
        _showAllButton.onClick.AddListener(ShowAll);
        _restartButton.onClick.AddListener(RestartGame);
        _pauseButton.onClick.AddListener(PauseGame);
        EventBus.Instance.OnGameEvent += HandleEvent;
    }

    private void OnDisable()
    {
        _showAllButton.onClick.RemoveListener(ShowAll);
        _pauseButton.onClick.RemoveListener(PauseGame);
        _restartButton.onClick.AddListener(RestartGame);
        EventBus.Instance.OnGameEvent -= HandleEvent;
    }

    private void HandleEvent(IEvent @event)
    {
        switch (@event)
        {
            case ScoreChanged score:
                ScoreChanged(score.Score);
                break;
            case GamePausedEvent pause:
                if (_gameStateService.WinScore == _gameStateService.CurrentScore)
                    ShowWin();
                break;
        }

    }

    private void ShowAll()
    {
        foreach (var ev in EventBus.Instance.Events)
        {
            Debug.Log(ev.GetDescription());
        }
    }

    private void ScoreChanged(int score) => _score.text = score.ToString();

    private void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    private void PauseGame() => _gameStateService.SetPauseState();

    private void ShowWin() => _winText.gameObject.SetActive(true);
}
