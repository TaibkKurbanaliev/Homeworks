using Mirror;
using System;
using UnityEngine;

public class InstanceInfo : NetworkBehaviour
{
    public event Action<string> OnNameChanged;
    public event Action<Color> OnColorChanged;
    public event Action<bool> OnReadyChanged;

    [SyncVar(hook = nameof(InstanceInfo_NameChanged)), SerializeField] private string _name;
    [SyncVar(hook = nameof(InstanceInfo_ColorChanged)), SerializeField] private Color _color;
    [SyncVar(hook = nameof(InstanceInfo_ReadyChanged)), SerializeField] private bool _isReady;

    public void SetName(string name)
    {
        CmdSetName(name);
    }

    public void SetColor(Color color)
    {
        CmdSetColor(color);
    }

    public void SetReady(bool isReady)
    {
        CmdSetReady(isReady);
    }

    private void InstanceInfo_ReadyChanged(bool prev, bool next)
    {
        if (prev == next)
            return;

        OnReadyChanged?.Invoke(next);
    }

    [Command]
    private void CmdSetReady(bool isReady)
    {
        _isReady = isReady;
    }

    private void InstanceInfo_ColorChanged(Color prev, Color next)
    {
        if (prev == next)
            return;

        OnColorChanged?.Invoke(next);
    }

    [Command]
    private void CmdSetColor(Color color)
    {
        _color = color;
    }

    private void InstanceInfo_NameChanged(string prev, string next)
    {
        if (prev == next)
            return;

        OnNameChanged?.Invoke(next);
    }

    [Command]
    private void CmdSetName(string name)
    {
        _name = name;
    }
}
