using UnityEngine;

[DefaultExecutionOrder(-15)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EnemySpawner _spawner;

    private PlayerInputActions _input;

    private void Awake()
    {
        _input = new PlayerInputActions();
        _input.Enable();
        _player.Init(_input);
        _spawner.Init(_player);
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
