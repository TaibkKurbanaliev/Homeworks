using UnityEngine;

public interface IGameModifier
{
    void OnEnterGameplay();
    void OnExitGameplay();
    void Tick(float deltaTime);
}
