using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour
{
    public const string k_GameScene = "Game";

    [SerializeField] private PlayerLobbyView _viewPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private int _minimumPlayerToStart = 1;
    [SerializeField] private SceneLoader _sceneLoader;

    private List<PlayerLobbyView> _playerViews = new List<PlayerLobbyView>();

    private void Awake()
    {
        NetworkManagerExt.singleton.ServerPlayerConnected += OnServerPlayerConnected;
        NetworkManagerExt.singleton.ServerPlayerDisconnected += OnServerPlayerDisconnected;
        _startGameButton.onClick.AddListener(OnStartGameClicked);
        _backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnDestroy()
    {
        NetworkManagerExt.singleton.ServerPlayerConnected -= OnServerPlayerConnected;
        NetworkManagerExt.singleton.ServerPlayerDisconnected -= OnServerPlayerDisconnected;
        _startGameButton.onClick.RemoveListener(OnStartGameClicked);
        _backButton.onClick.RemoveListener(OnBackClicked);
    }

    private void OnServerPlayerDisconnected(NetworkConnection conn)
    {
        var info = NetworkManagerExt.LocalPlayers[conn].GetComponent<InstanceInfo>();

        RpcRemoveDisconnectedPlayer(info);
    }

    private void OnServerPlayerConnected(NetworkConnection conn)
    {
        if (NetworkManagerExt.LocalPlayers.TryGetValue(conn, out var networkIdentity))
        {
            foreach (var player in NetworkManagerExt.LocalPlayers)
            {
                if (player.Key != conn)
                {
                    TargetPlayerConnected(conn, player.Value.GetComponent<InstanceInfo>());
                }
            }

            RpcPlayerConnected(networkIdentity.GetComponent<InstanceInfo>());
        }
    }

    private void OnBackClicked()
    {
        if (isServer)
            NetworkManagerExt.singleton.StopHost();
        else 
            NetworkManagerExt.singleton.StopClient();
    }

    [ClientRpc]
    private void RpcRemoveDisconnectedPlayer(InstanceInfo info)
    {
        var playerView = _playerViews.FirstOrDefault(pv => pv.Info == info);

        if (playerView != null)
        {
            _playerViews.Remove(playerView);
            Destroy(playerView.gameObject);
        }
    }

    private void OnStartGameClicked()
    {
        if (_playerViews.Count >= _minimumPlayerToStart && _playerViews.Count(view => view.Info.IsReady) == _playerViews.Count)
        {
            NetworkManagerExt.singleton.ServerChangeScene(k_GameScene);
        }
    }

    [TargetRpc]
    private void TargetPlayerConnected(NetworkConnection conn, InstanceInfo info)
    {
        AddPlayer(info);
    }

    [ClientRpc]
    private void RpcPlayerConnected(InstanceInfo info)
    {
        AddPlayer(info);
    }

    private void AddPlayer(InstanceInfo info)
    {
        if (info.IsLeader && info.isOwned)
            _startGameButton.gameObject.SetActive(true);

        var playerView = Instantiate(_viewPrefab, _container);
        playerView.Init(info);
        _playerViews.Add(playerView);
    }
}
