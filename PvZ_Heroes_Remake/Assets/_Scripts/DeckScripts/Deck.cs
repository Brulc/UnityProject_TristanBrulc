using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck 
{
    private const int deckLimit = 40;
    public int cardLimit = 10;
    public List<CardInfo> deckList;
    public Dictionary<CardInfo, int> deckCount;

    public Deck ()
    {
        deckList = new();
        deckCount = new();
    }
    public Deck ( List<CardInfo> deckList)
    {
        this.deckList = new();
        deckCount = new();
        foreach ( CardInfo card in deckList )
        {
            if ( this.deckList.Count <= deckLimit )
            {
                if ( deckCount.ContainsKey(card) && deckCount[card] < cardLimit )
                {
                    this.deckList.Add(card);
                    deckCount[card]++;
                }
                else if ( !deckCount.ContainsKey(card) )
                {
                    this.deckList.Add(card);
                    deckCount.Add(card,1);
                }
            }
        }
    }
    public void AddCard( CardInfo card )
    {
        if ( deckList.Count <= deckLimit )
        {
            if ( deckCount.ContainsKey(card) && deckCount[card] < cardLimit )
            {
                deckList.Add(card);
                deckCount[card]++;
            }
            else if ( !deckCount.ContainsKey(card) )
            {
                deckList.Add(card);
                deckCount.Add(card,1);
            }
        }
        
        
    }
    public void RemoveCard( CardInfo card )
    {
        if ( deckCount.ContainsKey(card) && deckList.Count > 0 )
        {
            deckList.Remove(card);
            deckCount[card]--;
        }
    }
}
