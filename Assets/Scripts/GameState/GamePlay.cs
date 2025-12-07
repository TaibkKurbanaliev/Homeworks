using UnityEngine;

public class Gameplay : MonoBehaviour
{
    private StateMachine _stateMachine;

    [field: SerializeField] public int MaxWaves { get; private set; } = 5;
    public int NumberOfAliveZombies { get; private set; }
    public int Score { get; private set; }

    private void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new StartState(_stateMachine));
        _stateMachine.AddState(new PlayingState(_stateMachine, this));
        _stateMachine.AddState(new WinState());
        _stateMachine.AddState(new LoseState());
        _stateMachine.SwitchState<StartState>();
    }

    private void Update() => _stateMachine.Update();
}
