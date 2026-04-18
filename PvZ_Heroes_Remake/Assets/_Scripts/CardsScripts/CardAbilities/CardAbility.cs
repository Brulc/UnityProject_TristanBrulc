using UnityEngine;


public abstract class CardAbility : ScriptableObject
{
    public AbilityType abilityType;
    public abstract void Execute( Player player, ICardTarget target ); 
}

