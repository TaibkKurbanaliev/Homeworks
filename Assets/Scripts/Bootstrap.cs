using TMPro;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    /*[SerializeField] private Player _player;
    [SerializeField] private TMP_Dropdown _inputTypes;
    [SerializeField] private Transform _target;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private MainMenuView _menuView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private GameplayHUD _hudView;

    private GameStateMachine _stateMachine;

    private void Awake()
    {
        _player.Construct(_input, _movement, _health, _loggerService);

        _loggerService.Log("StartGame");

        _hudView.Construct(_health);

        _stateMachine = new GameStateMachine();
        _stateMachine.AddState(new MainMenuState(_stateMachine, _menuView));
        _stateMachine.AddState(new GameplayState(_stateMachine, _hudView, _health));
        _stateMachine.AddState(new PauseState(_stateMachine, _pauseView));
        _stateMachine.AddState(new GameOverState(_stateMachine, _gameOverView));
        _stateMachine.SwitchState<MainMenuState>();
    }*/
}
