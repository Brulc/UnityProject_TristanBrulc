using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroInfo", menuName = "Scriptable Objects/HeroInfo")]
public class HeroInfo : ScriptableObject
{
    public int HeroID;
    public Team team;
    public Class class1;
    public Class class2;
    public Sprite FullImage;
    public Sprite CardImage;

    public List<PremadeDeck> premadeDecks;
}
