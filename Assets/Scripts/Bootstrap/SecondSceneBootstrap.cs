using UnityEngine;

public class SecondSceneBootstrap : Bootstrap
{
    [SerializeField] private Crosshair _crosshair;
    [SerializeField] private PlayerController _playerController;

    protected override void Init()
    {
        base.Init();
        Cursor.visible = false;
        _crosshair.Init(PlayerInputActions);
        _playerController.Init(PlayerInputActions);
    }
}
