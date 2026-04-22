using UnityEngine;
using System.Collections.Generic;

public class DeckBuilderManager : MonoBehaviour
{
    public static DeckBuilderManager Instance;
    private Dictionary<CardInfo, int> cardDictionary;


    [HideInInspector] public Deck deck;
    [HideInInspector] public HeroInfo chosenHero;
    
    #region Databases
    [SerializeField] private HeroDatabase womanHeroes;
    [SerializeField] private HeroDatabase manHeroes;
    [SerializeField] private CardDatabase cardPool;
    #endregion
    

    void Start()
    {
        Instance = this;
        deck = new();
        cardDictionary = new();

        HeroDatabase tmpDatabase = (SessionChoices.Instance.chosenTeam == Team.Man)? manHeroes:womanHeroes;
        foreach ( var hero in tmpDatabase.heroList )
        {
            DeckBuilderUIManager.Instance.DrawHeroButton(hero);
        }
    }
    public void DrawAvailableCards()
    {
        foreach( CardInfo card in cardPool.allCards )
        {
            if ( card.cardClass == chosenHero.class1 || card.cardClass == chosenHero.class2 )
            {
                if ( cardDictionary.ContainsKey(card))
                {
                    cardDictionary.Add(card, SessionChoices.Instance.deck.cardLimit);
                    DeckBuilderUIManager.Instance.DrawStartCards(
                        card, 
                        cardDictionary[card] - ((deck.deckCount.ContainsKey(card))? deck.deckCount[card]:0)
                    );
                    /*
                    cardButton = Instantiate( cardButton, availableCards.transform );
                    cardButton.Initialize(card, cardDictionary[card] - (
                        (deck.deckCount.ContainsKey(card))? deck.deckCount[card]:0),
                        false 
                    );
                    cardButtons.Add(cardButton);
                    availableButtonDictionary.Add(card, cardButton);
                    */
                }
            }
        }
    }
    public void ResetDeckBuilder()
    {
        deck = new();
        cardDictionary = new();
    }
    public void ConfirmDeck()
    {
        if ( deck.deckList.Count == 40 )
        {
            SessionChoices.Instance.chosenHero = chosenHero;
            SessionChoices.Instance.deck = deck;
            MenuManager.Instance.ToBattleMenu();
        }
    }
    public void Cancel()
    {
        MenuManager.Instance.ToBattleMenu();
    }
}
