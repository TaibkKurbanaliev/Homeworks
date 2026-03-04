using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "PeriodicDamageEffect", menuName = "Effects/PeriodicDamageEffect")]
public class PeriodicDamageEffect : Effect
{
    [SerializeField] private float _damage;

    private Dictionary<Player, CancellationTokenSource> _tokens = new();

    public override void ApplyEffect(Player player)
    {
        _tokens[player] = new CancellationTokenSource();
        StartEffect(player, _tokens[player].Token).Forget();
    }

    public override void StopEffect(Player player)
    {
        _tokens[player].Cancel();
        _tokens.Remove(player);
    }

    private async UniTask StartEffect(Player player, CancellationToken token = default)
    {
        Debug.Log("started Period");
        while (!token.IsCancellationRequested)
        {
            Debug.Log("Damage" + _damage);
            player.Health.TakeDamage(_damage);
            await UniTask.WaitForSeconds(Interval, cancellationToken: token);
        }
    }
}
