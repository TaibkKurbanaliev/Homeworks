using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class NoInputModifier : IGameModifier
{
    private float _workTime = 3f;
    private float _timer;
    private float _reloadTime = 10f;

    private bool _isDisabled;
    private bool _isReloading;

    private IInputService _inputService;

    private CancellationTokenSource _cts;

    public NoInputModifier(IInputService input)
    {
        _inputService = input;
    }

    public void OnEnterGameplay()
    {
        _isReloading = true;
        _isDisabled = false;
        _cts = new CancellationTokenSource();
    }

    public void OnExitGameplay()
    {
        _isDisabled = true;
        _cts?.Cancel();
    }

    public void Tick(float deltaTime)
    {
        if (_isDisabled)
            return;

        if (_isReloading)
            _timer += deltaTime;
            
        if (_timer >= _reloadTime)
        {
            _timer = 0f;
            _ = SwapInput();
            _isReloading = false;
        }

    }

    private async Task SwapInput()
    {
        _inputService.SetActive(false);

        await Task.Delay((int)(_workTime * 1000), _cts.Token);

        _inputService.SetActive(true);
        _isReloading = true;
    }
}
