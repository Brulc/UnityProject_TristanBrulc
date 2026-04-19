using System.Collections.Generic;
using UnityEngine;

public class Player : IDamabagle, IHealable, ITeamAfiiliation
{
    public HeroInfo hero;
    public Deck referenceDeck;
    public List<CardInfo> deck;
    public List<CardInfo> hand;
    public int maxHealth;
    public int health;
    public int mana;
    public int shield;
    public Player ( HeroInfo hero, Deck deck, int health )
    {
        this.hero = hero;
        referenceDeck = deck;
        this.health = health;
        maxHealth = health;
        ShuffleDeck();
    }
    private void ShuffleDeck()
    {
        deck = new();
        foreach ( CardInfo card in referenceDeck.deckList)
        {
            deck.Insert( (int)(Random.Range(0, deck.Count)), card );
        }
    }
    public void InitializeHand(int startingCards)
    {
        hand = new();
        for ( int i = 0; i < startingCards; i++ )
        {
            hand.Add(deck[0]);
            deck.Remove(deck[0]);
        }
    }
    public void DrawCard()
    {
        hand.Add(deck[0]);
        deck.Remove(deck[0]);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if ( health <= 0 )
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        health += amount;
        if ( health > maxHealth) health = maxHealth;
    }
    public void Die()
    {
        Debug.Log("I am dead!");
    }
    public Team GetTeam() { return hero.team; }
    public bool IsDead() { return health <= 0; }
}
