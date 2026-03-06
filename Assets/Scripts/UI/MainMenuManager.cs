using Mirror;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _joinButton;
    [SerializeField] private Button _connectButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private TMP_InputField _ipInput;

    [SerializeField] private GameObject _joinMenu;
    [SerializeField] private GameObject _buttonsMenu;

    private void Awake()
    {
        _createLobbyButton.onClick.AddListener(OnCreateLobbyPressed);
        _joinButton.onClick.AddListener(OnJoinLobbyPressed);
        _connectButton.onClick.AddListener(OnConnectButtonPressed);
        _exitButton.onClick.AddListener(OnExitPressed);
        _backButton.onClick.AddListener(OnBackButtonPressed);
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        
    }

    private void OnBackButtonPressed()
    {
        _buttonsMenu.SetActive(true);
        _joinMenu.SetActive(false);
    }

    private void OnCreateLobbyPressed()
    {
        NetworkManagerExt.singleton.StartHost();
    }

    private void OnJoinLobbyPressed()
    {
        _buttonsMenu.SetActive(false);
        _joinMenu.SetActive(true);
    }

    private void OnConnectButtonPressed()
    {
        NetworkManagerExt.singleton.networkAddress = _ipInput.text;
        NetworkManagerExt.singleton.StartClient();
    }

    private void OnExitPressed()
    {
        Application.Quit();
    }
}
