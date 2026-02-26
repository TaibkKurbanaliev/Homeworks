using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private PlayerLeaderboardView _prefab;
    [SerializeField] private LeaderboardModel _model;
    [SerializeField] private Transform _container;

    private List<PlayerLeaderboardView> _views = new();


    public void Show()
    {
        gameObject.SetActive(true);
        Redraw();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Clear();
    }

    private void Redraw()
    {
        Clear();

        foreach (var player in _model.ServerPlayers)
        {
            var view = Instantiate(_prefab, _container);
            view.Init(player.PlayerInfo, player.InstanceInfo);
            _views.Add(view);
        }
    }

    private void Clear()
    {
        foreach (var view in _views)
        {
            Destroy(view.gameObject);
        }

        _views.Clear();
    }

}
