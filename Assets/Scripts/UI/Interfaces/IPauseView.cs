using System;
using UnityEngine;

public interface IPauseView : IView
{
    event Action ResumeClicked;
    event Action MenuClicked;
}
