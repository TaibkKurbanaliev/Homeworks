using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ServicesSOAP", menuName = "Scriptable Objects/ServicesSOAP")]
public class ServicesSOAP : ScriptableObject
{
    public InputSystem_Actions Actions {  get; private set; }
    public ILoggerService LoggerService { get; private set; }
    public IInputService Input { get; private set; }
    public IMovementService Movement { get; private set; }
    public IHealth Health { get; private set; }
    public ICollectibleService Collectible { get; private set; }

    public void GlobalServicesRegister(ILoggerService loggerService, IInputService input, 
                                       ICollectibleService collectible, InputSystem_Actions actions)
    {
        LoggerService = loggerService;
        Input = input;
        Collectible = collectible;
        Actions = actions;
    }

    public void PlayerServicesRegister(IMovementService movement, IHealth health)
    {
        Movement = movement;
        Health = health;
    }

    public void ChangeInputService(IInputService service)
    {
        Input = service;
    }
}
