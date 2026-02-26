using System;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private LeaderboardView _view;
    [SerializeField] private LeaderboardModel _model;

    private IInput _input;

    public void Init(IInput input)
    {
        _input = input;
        _input.TabClosed += OnTabClosed;
        _input.TabOpenned += OnTabOpenned;
    }

    public void OnDestroy()
    {
        _input.TabClosed -= OnTabClosed;
        _input.TabOpenned -= OnTabOpenned;
    }

    private void OnTabOpenned()
    {
        _view.Show();
    }

    private void OnTabClosed()
    {
        _view.Hide();
    }
}
