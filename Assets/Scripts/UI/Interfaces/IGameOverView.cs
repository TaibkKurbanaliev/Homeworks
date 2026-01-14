using System;
using UnityEngine;

public interface IGameOverView : IView
{
    event Action RestartClicked;
    event Action MenuClicked;
}
