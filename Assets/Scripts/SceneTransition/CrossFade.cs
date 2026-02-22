using Cysharp.Threading.Tasks;
using DG.Tweening;
using Edgegap;
using UnityEngine;

public class CrossFade : SceneTransition
{
    [SerializeField] private float _animDuration;
    [SerializeField] private CanvasGroup _lobby;
    [SerializeField] private CanvasGroup _loading;

    public override async UniTask AnimationIn()
    {
        await _lobby.DOFade(0f, _animDuration);
        _lobby.blocksRaycasts = false;
        _lobby.gameObject.SetActive(true);
        await _loading.DOFade(1f, _animDuration).ToUniTask();
    }

    public override async UniTask AnimationOut()
    {
        await _loading.DOFade(0f, 2f).ToUniTask();
    }
}
