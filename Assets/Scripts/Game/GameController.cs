using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static Unity.Cinemachine.CinemachineFreeLookModifier;
using static UnityEngine.Rendering.DebugUI;

public class GameController
{
    private IEntityFactory<Collectible> _coinFactory;

    private ServicesSOAP _services;
    private CollectibleConfig _coinConfig;
    private Player _player;
    private Spawner _spawner;
    private ICollectibleService _collectible;
    private List<IGameModifier> _modifiers = new();
    private ModifierConfig _modifierConfig;

    public GameController(ServicesSOAP services, Spawner spawner, CollectibleConfig coinConfig,
                          Player player, ICollectibleService collectible, ModifierConfig modifierConfig)
    {
        _coinConfig = coinConfig;
        _services = services;
        _player = player;
        _spawner = spawner;
        _collectible = collectible;
        _modifierConfig = modifierConfig;
        _modifierConfig.Init();
    }

    public void StartLevel()
    {
        _services.LoggerService.Log("GameStarted");
        _coinFactory = new CollectibleFactory(_coinConfig);
        _services.Health.Reset();
        _player.gameObject.SetActive(true);
        _player.Construct(_services.Input, _services.Movement, _services.Health, _services.LoggerService, _modifiers);
        _spawner.Construct(_coinFactory, _collectible);

        _modifiers.Clear();

        foreach (var modifier in _modifierConfig.Modifiers)
        {
            modifier.OnExitGameplay();
            modifier.OnEnterGameplay();
            _modifiers.Add(modifier);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeInputType(InputType type)
    {
        IInputService input;

        switch (type)
        {
            case InputType.Keyboard:
                input = new DefaultInputService(_services.Actions);
                break;
            case InputType.AI:
                var target = new Vector3(2f, 2f, 2f);
                input = new AIInputService(target, _player.transform);
                break;
            case InputType.FakeInput:
                input = new FakeInputService(Vector2.up);
                break;
            default:
                throw new NotImplementedException();
        }

        _player.ChangeInput(input);

        _services.LoggerService.Log("Change input type - " + type);
    }
}
