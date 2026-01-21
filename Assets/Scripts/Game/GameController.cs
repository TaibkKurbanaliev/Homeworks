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
    private readonly IModifiersView _modifiersView;

    public GameController(ServicesSOAP services, Spawner spawner, CollectibleConfig coinConfig,
                          Player player, ICollectibleService collectible, ModifierConfig modifierConfig, 
                          IModifiersView modifiersView)
    {
        _coinConfig = coinConfig;
        _services = services;
        _player = player;
        _spawner = spawner;
        _collectible = collectible;
        _modifierConfig = modifierConfig;
        _modifiersView = modifiersView;
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
        _modifiersView.Init();

        foreach (var modifier in _modifiers)
            modifier.OnExitGameplay();

        _modifiers.Clear();

        foreach (var modifier in _modifierConfig.Modifiers)
        {
            modifier.OnEnterGameplay();
            _modifiers.Add(modifier);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddModifier(ModifierType type)
    {
        var modifier = _modifierConfig.GetModifier(type);
        _modifiers.Add(_modifierConfig.GetModifier(type));
        modifier.OnEnterGameplay();
        _services.LoggerService.Log("Enable modifier - " + modifier.GetType());
    }

    public void RemoveModifier(int index)
    {
        _modifiers[index].OnExitGameplay();
        _services.LoggerService.Log("Disable modifier - " + _modifiers[index].GetType());
        _modifiers.RemoveAt(index);
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

        _services.ChangeInputService(input);
        _services.LoggerService.Log("Change input type - " + type);
    }
}
