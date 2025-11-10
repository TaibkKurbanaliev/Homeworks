using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayModeManager : MonoBehaviour
{
    private string LoadSceneName = "MainMenu";

    [SerializeField] private TMP_Text _spawnedUnitsCounter;
    [SerializeField] private TMP_Text _destroyedUnitsCounter;
    [SerializeField] private TMP_Text _gunFiresCounter;
    [SerializeField] private TMP_Text _recordDestroyedUnits;
    [SerializeField] private TMP_Text _recordShots;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private PlayerController _playerController;

    private PlayerInputActions _inputActions;

    private int _numberOfGunFires =  0;
    private int _numberOfSpawnUnits =  0;
    private int _numberOfDestroyedUnits =  0;


    public void Init(PlayerInputActions actions)
    {
        _inputActions = actions;
        _recordDestroyedUnits.text = PlayerPrefs.GetInt(nameof(_recordDestroyedUnits)).ToString();
        _recordShots.text = PlayerPrefs.GetInt(nameof(_recordShots)).ToString();
    }

    private void OnEnable()
    {
        _inputActions.UI.Cancel.performed += OnCancel;
        _inputActions.UI.Reload.performed += OnSceneReload;
        _spawner.TargetSpawned += OnTargetSpawned;
        _spawner.TargetDestroyed += OnTargetDestroyed;
        _playerController.Gun.Fired += OnGunFired;
    }

    private void OnDisable()
    {
        _inputActions.UI.Cancel.performed -= OnCancel;
        _inputActions.UI.Reload.performed -= OnSceneReload;
        _spawner.TargetSpawned -= OnTargetSpawned;
        _spawner.TargetDestroyed -= OnTargetDestroyed;
        _playerController.Gun.Fired -= OnGunFired;
    }

    private void OnTargetDestroyed()
    {
        _destroyedUnitsCounter.text = (++_numberOfDestroyedUnits).ToString();
    }

    private void OnGunFired()
    {
        _gunFiresCounter.text = (++_numberOfGunFires).ToString();
    }
    
    private void OnTargetSpawned()
    {
        _spawnedUnitsCounter.text = (++_numberOfSpawnUnits).ToString();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        if (int.Parse(_recordDestroyedUnits.text) <= _numberOfDestroyedUnits)
        {
            PlayerPrefs.SetInt(nameof(_recordShots), _numberOfGunFires);
            PlayerPrefs.SetInt(nameof(_recordDestroyedUnits), _numberOfDestroyedUnits);
        }
        
        StartCoroutine(LoadMainMenu());
    }

    private void OnSceneReload(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LoadMainMenu()
    {
        var sceneLoad = SceneManager.LoadSceneAsync(LoadSceneName);

        while (sceneLoad.progress < 0.9)
        {
            yield return null;
        }

        sceneLoad.allowSceneActivation = true;
    }
}
