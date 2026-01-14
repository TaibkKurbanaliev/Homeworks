using System;
using UnityEngine;

public interface IGameplayHUD : IView
{
    event Action PauseClicked;
}
