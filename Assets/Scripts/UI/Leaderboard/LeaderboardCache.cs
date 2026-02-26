using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardCache
{
    public readonly List<ServerPlayerConnectedToGame> Players = new();
    private LeaderboardModel _model;

    public void Bind(LeaderboardModel model)
    {
        Players.Clear();
        Players.AddRange(model.ServerPlayers);
        _model = model;
        model.ServerPlayers.OnAdd += OnAdd;
        model.ServerPlayers.OnRemove += OnRemove;
        model.ServerPlayers.OnSet += OnSet;
    }

    private void OnAdd(int index)
    {
        Players.Insert(index, _model.ServerPlayers[index]);
    }

    private void OnRemove(int index, ServerPlayerConnectedToGame player)
    {
        Players.RemoveAt(index);
    }

    private void OnSet(int index, ServerPlayerConnectedToGame player)
    {
        Players[index] = _model.ServerPlayers[index];
    }
}
