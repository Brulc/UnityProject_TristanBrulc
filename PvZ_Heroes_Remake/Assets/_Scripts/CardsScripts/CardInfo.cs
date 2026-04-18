using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Objects/New Card")]
public class CardInfo : ScriptableObject
{
    public CardType type;
    public Team cardTeam;
    public Class cardClass;
    public Rarity cardRarity;

    public string cardName;
    public Sprite cardImage;
    public int cost;
    
    public int health;
    public int attack;
    
    public List<CardAbility> abilities;
    public bool Amphibious;
    public bool Team_Up;
}

