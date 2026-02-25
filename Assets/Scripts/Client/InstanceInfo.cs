using Mirror;
using System;
using UnityEngine;

public class InstanceInfo : NetworkBehaviour
{
    public event Action<string> NameChanged;
    public event Action<Color> ColorChanged;
    public event Action<bool> ReadyChanged;

    [SyncVar(hook = nameof(OnNameChanged)), SerializeField] private string _name;
    [SyncVar(hook = nameof(OnColorChanged)), SerializeField] private Color _color;
    [SyncVar(hook = nameof(OnReadyChanged)), SerializeField] private bool _isReady;
    [SyncVar] private bool _isLeader;

    private InstanceSavedInfo _playerInfo;

    public string Name => _name;
    public Color Color => _color;
    public bool IsReady => _isReady;
    public bool IsLeader => _isLeader;

    [Server]
    public void Init(InstanceSavedInfo info)
    {
        _playerInfo = info;
        _name = info.Name;
        _color = info.Color;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        EventBus<InstanceConnectedEvent>.Raise(new InstanceConnectedEvent { Info = this });
    }

    public void SetName(string name)
    {
        CmdSetName(name);
    }

    public void SetColor(Color color)
    {
        CmdSetColor(color);
    }

    public void SetReady()
    {
        CmdSetReady();
    }

    [Server]
    public void SetLeader()
    {
        _isLeader = true;
    }

    private void OnReadyChanged(bool prev, bool next)
    {
        if (prev == next)
            return;

        ReadyChanged?.Invoke(next);
    }


    [Command]
    private void CmdSetReady()
    {
        _isReady = !_isReady;
    }

    private void OnColorChanged(Color prev, Color next)
    {
        if (prev == next)
            return;

        ColorChanged?.Invoke(next);
    }

    [Command]
    private void CmdSetColor(Color color)
    {
        _color = color;
        _playerInfo.Color = color;
    }

    private void OnNameChanged(string prev, string next)
    {
        if (prev == next)
            return;

        NameChanged?.Invoke(next);
    }

    [Command]
    private void CmdSetName(string name)
    {
        _name = name;
        _playerInfo.Name = name;
    }
}
