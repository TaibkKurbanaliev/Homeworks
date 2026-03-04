using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SlowEffect", menuName = "Effects/SlowEffect")]
public class SlowEffect : Effect
{
    [SerializeField, Range(0,100)] private float _slowPercent;
    
    private StatModifierData _modifier;

    private void Awake()
    {
        _modifier = new StatModifierData(StatType.Speed, OperationType.Multiply, _slowPercent / 100f);
    }

    public override void ApplyEffect(Player player)
    {
        var conn = NetworkManagerExt.LocalPlayers.FirstOrDefault(netPlayer =>
                                                          netPlayer.Value.GetComponent<ClientInstance>().CurrentPlayer == player).Key;
        player.StatMediator.AddModifier(conn, _modifier);
    }

    public override void StopEffect(Player player)
    {
        var conn = NetworkManagerExt.LocalPlayers.FirstOrDefault(netPlayer =>
                                                          netPlayer.Value.GetComponent<ClientInstance>().CurrentPlayer == player).Key;
        player.StatMediator.RemoveModifier(conn, _modifier);
    }
}
