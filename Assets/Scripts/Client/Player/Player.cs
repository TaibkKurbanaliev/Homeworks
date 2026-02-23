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

    public void Init(InstanceInfo info)
    {
        RpcInit(info);
    }

    [ClientRpc]
    private void RpcInit(InstanceInfo info)
    {
        _playerView.Init(info);
    }
}
