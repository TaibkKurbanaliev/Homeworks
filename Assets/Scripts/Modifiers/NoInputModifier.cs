using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class NoInputModifier : IGameModifier
{
    private NoInputModifierConfig _config;
    private float _timer;

    private bool _isDisabled;
    private bool _isReloading;

    private IInputService _inputService;

    private CancellationTokenSource _cts;

    public NoInputModifier(IInputService input, NoInputModifierConfig config)
    {
        _inputService = input;
        _config = config;
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
            
        if (_timer >= _config.ReloadTime)
        {
            _timer = 0f;
            _ = SwapInput();
            _isReloading = false;
        }

    }

    private async Task SwapInput()
    {
        _inputService.SetActive(false);

        await Task.Delay((int)(_config.NotWorkingTime * 1000), _cts.Token);

        _inputService.SetActive(true);
        _isReloading = true;
    }
}
