using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const string LoadSceneName = "Game";

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _setting;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Slider _reloadTime;
    [SerializeField] private TMP_Text _reloadSpeedValue;

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _reloadTime.value = PlayerPrefs.GetFloat(nameof(_reloadTime), 1f);
        _reloadSpeedValue.text = _reloadTime.value.ToString();

        _playButton.onClick.AddListener(OnPlayButtonClick);
        _settingButton.onClick.AddListener(OnSettingClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _applyButton.onClick.AddListener(OnApplyButtonClick);
        _reloadTime.onValueChanged.AddListener(OnReloadSpeedValueChanged);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _settingButton.onClick.RemoveListener(OnSettingClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _applyButton.onClick.RemoveListener(OnApplyButtonClick);
        _reloadTime.onValueChanged.RemoveListener(OnReloadSpeedValueChanged);
    }

    private void OnApplyButtonClick()
    {
        PlayerPrefs.SetFloat(nameof(_reloadTime), _reloadTime.value);
        _mainMenu.SetActive(true);
        _setting.SetActive(false);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnPlayButtonClick()
    {
        StartCoroutine(LoadPlayScene());
    }

    private void OnSettingClick()
    {
        _mainMenu.SetActive(false);
        _setting.SetActive(true);
    }

    private IEnumerator LoadPlayScene()
    {
        var sceneLoad = SceneManager.LoadSceneAsync(LoadSceneName);

        while(sceneLoad.progress < 0.9)
        {
            yield return null;
        }

        sceneLoad.allowSceneActivation = true;
    }

    public void OnReloadSpeedValueChanged(float value)
    {
        _reloadSpeedValue.text = value.ToString();
    }
}
