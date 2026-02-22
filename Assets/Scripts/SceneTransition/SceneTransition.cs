using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class SceneTransition
{
    public abstract UniTask AnimationIn();
    public abstract UniTask AnimationOut();
}
