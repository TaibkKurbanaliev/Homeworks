using UnityEngine;

public class PauseState : IState
{
    private GameStateMachine _stateMachine;

    public PauseState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }
}
