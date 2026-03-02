using Cysharp.Threading.Tasks;
using Mirror;
using System.Threading;
using TMPro;
using UnityEngine;

public class Ping : MonoBehaviour
{
    [SerializeField] private TMP_Text _ping;
    [SerializeField] private float _updateDelay;

    private CancellationTokenSource _token;

    private void Start()
    {
        PingUpdate().Forget();
    }

    private async UniTask PingUpdate()
    {
        while (!destroyCancellationToken.IsCancellationRequested)
        {
            if (!NetworkClient.active)
                continue;

            _ping.text = Mathf.RoundToInt((float)(NetworkTime.rtt * 1000)).ToString();

            await UniTask.WaitForSeconds(_updateDelay);
        }
    }
}
