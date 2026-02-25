using Mirror;
using System;
using UnityEngine;

public class PlayerInfo : NetworkBehaviour
{
    public event Action<int> NumberOfKillsChanged;
    public event Action<int> NumberOfDeathsChanged;

    [SyncVar(hook = nameof(OnNumberOfKills))] private int _numberOfKills;
    [SyncVar(hook = nameof(OnNumberOfDeaths))] private int _numberOfDeaths;

    public int NumberOfKills => _numberOfKills; 
    public int NumberOfDeaths => _numberOfDeaths;

    [Server]
    public void AddKill() => _numberOfKills++;

    [Server]
    public void AddDeath() => _numberOfDeaths++;

    private void OnNumberOfKills(int prev, int next)
    {
        if (prev == next) return;

        NumberOfKillsChanged?.Invoke(next);
    }

    private void OnNumberOfDeaths(int prev, int next)
    {
        if (prev == next) return;

        NumberOfDeathsChanged?.Invoke(next);
    }
}
