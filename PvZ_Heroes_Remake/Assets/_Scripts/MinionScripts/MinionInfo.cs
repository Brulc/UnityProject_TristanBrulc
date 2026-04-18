using UnityEngine;

public class MinionInfo : IDamabagle, IHealable, ITeamAfiiliation
{
    public CardInfo card;
    public int health;
    public int attack;
    public int baseHealth;
    public int baseAttack;
    public GameObject minionDisplay;
    public MinionInfo (CardInfo cardInfo)
    {
        card = cardInfo;
        baseHealth = card.health;
        baseAttack = card.attack;
        attack = baseAttack;
        health = baseHealth;
    }
    public void Attack ( IDamabagle target )
    {
        if ( target != null )
        {
            target.TakeDamage(attack);
        }
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if ( health <= 0)
        {
            Die();
        }
    }
    public void Heal ( int amount )
    {
        health += amount;
        if ( health > baseHealth) health = baseHealth;
    }
    public void Die()
    {
        
    }
    public bool IsDead() { return health <= 0; }
    public Team GetTeam() { return card.cardTeam; }
}
