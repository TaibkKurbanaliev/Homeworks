using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class NoInputModifier : IGameModifier
{
    private NoInputModifierConfig _config;
    private float _timer;

    private bool _isDisabled;
    private bool _isReloading;

    private ServicesSOAP _services;

    private CancellationTokenSource _cts;

    public NoInputModifier(ServicesSOAP services,NoInputModifierConfig config)
    {
        _config = config;
        _services = services;
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
        _services.Input.SetActive(true);
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
        _services.Input.SetActive(false);

        await Task.Delay((int)(_config.NotWorkingTime * 1000), _cts.Token);

        _services.Input.SetActive(true);
        _isReloading = true;
    }
}
