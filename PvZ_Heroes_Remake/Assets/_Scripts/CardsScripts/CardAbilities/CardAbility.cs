using UnityEngine;


public abstract class CardAbility : ScriptableObject
{
    public AbilityType abilityType;
    [TextArea] public string description;
    public abstract void Execute( Player player, ICardTarget target ); 
}

