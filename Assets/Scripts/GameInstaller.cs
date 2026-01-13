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

        IInputService input = new DefaultInputService(_actions);
        IMovementService movement = new RigidbodyMovement(_player.GetComponent<Rigidbody>());
        IHealth health = new Health(100f);

        _player.Construct(input, movement, health);
    }

    private void OnDisable()
    {
        _actions.Disable();
    }
}
