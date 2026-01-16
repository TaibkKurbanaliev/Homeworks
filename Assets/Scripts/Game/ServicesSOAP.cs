using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ServicesSOAP", menuName = "Scriptable Objects/ServicesSOAP")]
public class ServicesSOAP : ScriptableObject
{
    public ILoggerService LoggerService { get; private set; }
    public IInputService Input { get; private set; }
    public IMovementService Movement { get; private set; }
    public IHealth Health { get; private set; }

    public void GlobalServicesRegister(ILoggerService loggerService, IInputService input)
    {
        LoggerService = loggerService;
        Input = input;
    }

    public void PlayerServicesRegister(IMovementService movement, IHealth health)
    {
        Movement = movement;
        Health = health;
    }
}
