using UnityEngine;

public class ConsoleLogger : ILoggerService
{
    public void Log(string message)
    {
        Debug.Log($"LOG MESSAGE: {message}");
    }
}
