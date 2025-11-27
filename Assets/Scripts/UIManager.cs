using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private TMP_Text _loseText;
    [SerializeField] private TMP_Text _numberOfHealth;
    [SerializeField] private TMP_Text _currentGameState;
    [SerializeField] private Button _showAllButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _continueButton;

    private GameStateService _gameStateService;
    private bool _isPaused;

    public void Init(GameStateService service)
    {
        _gameStateService = service;
    }

    private void OnEnable()
    {
        _showAllButton.onClick.AddListener(ShowAll);
        _restartButton.onClick.AddListener(RestartGame);
        _pauseButton.onClick.AddListener(PauseGame);
        _continueButton.onClick.AddListener(PauseGame);
        EventBus.Instance.OnGameEvent += HandleEvent;
    }

    private void OnDisable()
    {
        _showAllButton.onClick.RemoveListener(ShowAll);
        _pauseButton.onClick.RemoveListener(PauseGame);
        _continueButton.onClick.RemoveListener(PauseGame);
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
            case GameWinEvent win:
                _currentGameState.text = win.GetDescription();
                ShowWin();
                break;
            case GameLoseEvent lose:
                _currentGameState.text = lose.GetDescription();
                ShowLose();
                break;
            case PlayerDamaged player:
                _numberOfHealth.text = player.RemainingLives.ToString();
                break;
            case GameInitEvent init:
                _currentGameState.text = init.GetDescription(); 
                break;
            case GamePlayingEvent play:
                _currentGameState.text = play.GetDescription();
                break;
            case GamePausedEvent pause:
                _currentGameState.text = pause.GetDescription();
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

    private void PauseGame()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            _pauseButton.gameObject.SetActive(false);
            _continueButton.gameObject.SetActive(true);
        }
        else
        {
            _pauseButton.gameObject.SetActive(true);
            _continueButton.gameObject.SetActive(false);
        }

        _gameStateService.SetPauseState();
    }

    private void ShowWin() => _winText.gameObject.SetActive(true);
    private void ShowLose() => _loseText.gameObject.SetActive(true);
}
