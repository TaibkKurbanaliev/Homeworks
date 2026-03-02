using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EndGameController : NetworkBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _exit;

    private void Awake()
    {
        _restart.onClick.AddListener(OnRestartClicked);
        _exit.onClick.AddListener(OnExitClicked);
    }

    [TargetRpc]
    public void TargetRpcShowEndGame(NetworkConnection conn)
    {
        _view.SetActive(true);

        if (isServer)
            _restart.gameObject.SetActive(true);
    }

    private void OnRestartClicked()
    {
        NetworkManagerExt.singleton.ServerChangeScene(SceneName.Game.ToString());
    }

    private void OnExitClicked()
    {
    }
}
