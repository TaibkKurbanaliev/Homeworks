using UnityEngine;

public class GameplayState : IState
{
    private GameStateMachine _stateMachine;

    public GameplayState(GameStateMachine stateMachine)
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
