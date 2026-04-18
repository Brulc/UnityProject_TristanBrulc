using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PremadeDeck", menuName = "Scriptable Objects/PremadeDeck")]
public class PremadeDeck : ScriptableObject
{
    public List<CardInfo> deckList;
}
