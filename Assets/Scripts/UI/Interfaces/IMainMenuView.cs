using System;
using UnityEngine;

public interface IMainMenuView : IView
{
    event Action StartClicked;
    event Action ExitClicked;
}
