using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _joinButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _createLobbyButton.onClick.AddListener(OnCreateLobbyPressed);
        _joinButton.onClick.AddListener(OnJoinLobbyPressed);
        _exitButton.onClick.AddListener(OnExitPressed);
    }

    private void OnCreateLobbyPressed()
    {
        NetworkManagerExt.singleton.StartHost();
    }

    private void OnJoinLobbyPressed()
    {
    }

    private void OnExitPressed()
    {
        Application.Quit();
    }
}
