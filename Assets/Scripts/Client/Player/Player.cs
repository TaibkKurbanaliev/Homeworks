using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerView))]
public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerView _playerView;

    protected override void OnValidate()
    {
        if (_playerView == null)
            _playerView = GetComponent<PlayerView>();
    }

    public void Init(InstanceInfo _info)
    {
        _playerView.Init(_info);
    }
}
