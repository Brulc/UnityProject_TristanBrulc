using UnityEngine;

[CreateAssetMenu(fileName = "DealDamageAbility", menuName = "Card Abilities/DealDamageAbility")]
public class DealDamageAbility : CardAbility
{
    [SerializeField] private int damageAmount;
    
    override public void Execute(Player player, ICardTarget target)
    {
        if ( target is IDamabagle damagable && target is ITeamAfiiliation team && team.GetTeam() != player.GetTeam() )
            damagable.TakeDamage(damageAmount);
    }

}
