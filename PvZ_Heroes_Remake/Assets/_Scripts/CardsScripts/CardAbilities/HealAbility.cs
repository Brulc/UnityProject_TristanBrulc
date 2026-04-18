using UnityEngine;

[CreateAssetMenu(fileName = "HealAbility", menuName = "Card Abilities/HealAbility")]
public class HealAbility : CardAbility
{
    public int healAmount;
    override public void Execute(Player player, ICardTarget target)
    {
        if ( target is IHealable healable && target is ITeamAfiiliation team && team.GetTeam() == player.hero.team )
        {
                healable.Heal(healAmount);
        }
    }
}
