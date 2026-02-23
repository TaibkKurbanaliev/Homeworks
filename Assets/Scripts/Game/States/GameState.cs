using UnityEngine;

public class GameState : IState
{
    protected GameManager GameManager { get; private set; }
    protected IStateSwitcher StateSwitcher { get; private set; }

    public GameState(GameManager gameManager, IStateSwitcher stateSwitcher)
    {
        GameManager = gameManager;
        StateSwitcher = stateSwitcher;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void Update()
    {
    }
}
