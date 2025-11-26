using UnityEngine;

public class Init : IState
{
    private IStateSwitcher _switcher;

    public Init(IStateSwitcher switcher)
    {
        _switcher = switcher;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Update()
    {
        _switcher.SwitchState<Playing>();
    }
}
