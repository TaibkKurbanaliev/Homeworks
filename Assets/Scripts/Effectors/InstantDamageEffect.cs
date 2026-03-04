using UnityEngine;


[CreateAssetMenu(fileName = "InstantDamageEffect", menuName = "Effects/InstantDamageEffect")]
public class InstantDamageEffect : Effect
{
    [SerializeField] private float _damage;

    public override void ApplyEffect(Player player)
    {
        player.Health.TakeDamage(_damage);
    }

    public override void StopEffect(Player player)
    {
    }
}
