using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameInstaller : MonoBehaviour
{
    public Player _player;
    private InputSystem_Actions _actions;

    public void Awake()
    {
        _actions = new InputSystem_Actions();
        _actions.Enable();

        _player.Construct(new DefaultInputService(_actions));
    }

    private void OnDisable()
    {
        _actions.Disable();
    }
}
