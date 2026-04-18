using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeckBuilderUIManager : MonoBehaviour
{
    [SerializeField] private GameObject heroButtonsParent;
    [SerializeField] private HeroDatabase womanHeroes;
    [SerializeField] private HeroDatabase manHeroes;
    [SerializeField] private HeroSelectButton button;
    [SerializeField] private CardDatabase cardPool;
    [SerializeField] private CardButton referenceCardButton;
    private CardButton cardButton;
    [SerializeField] private GameObject availableCards;
    [SerializeField] private GameObject deckCards;

    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private MenuManager menuManager;

    private Dictionary<CardInfo, int> cardDictionary;
    [HideInInspector] public Dictionary<CardInfo, CardButton> deckButtonDictionary;
    [HideInInspector] public Dictionary<CardInfo, CardButton> availableButtonDictionary;

    [HideInInspector] public Deck deck;
    [HideInInspector] public HeroInfo chosenHero;

    void Awake ()
    {
        HeroDatabase tmpDatabase = (SessionChoices.Instance.chosenTeam == Team.Man)? manHeroes:womanHeroes;
        foreach ( var hero in tmpDatabase.heroList)
        {
            HeroSelectButton newButton = Instantiate(button, heroButtonsParent.transform);
            newButton.SetHero(hero);
            newButton.Initialize();
        }
        deck = new();
        cardDictionary = new();
        deckButtonDictionary = new();
        availableButtonDictionary = new();
        cardButton = referenceCardButton;
        foreach(CardInfo card in cardPool.allCards)
        {
            cardDictionary.Add(card, SessionChoices.Instance.deck.cardLimit);
        }
    }
    public void DrawAvailableCards() // nujno jih zapiši v list da jih lahko odstraniš al pa razdel v UI in logic
    {
        foreach( CardInfo card in cardPool.allCards )
        {
            if ( card.cardClass == chosenHero.class1 || card.cardClass == chosenHero.class2 )
            {
                if ( cardDictionary.ContainsKey(card))
                {
                    cardButton = Instantiate( cardButton, availableCards.transform );
                    cardButton.Initialize(card, cardDictionary[card] - (
                        (deck.deckCount.ContainsKey(card))? deck.deckCount[card]:0),
                        false 
                    );
                    availableButtonDictionary.Add(card, cardButton);
                }
            }
        }
    }
    public void DrawCard(CardInfo card, bool addCard)
    {
        if (addCard)
        {
            if ( !deckButtonDictionary.ContainsKey(card))
            {
                cardButton = Instantiate( cardButton, deckCards.transform );
                cardButton.Initialize(card, 1, true);
                cardButton.DisableButtons();
                deckButtonDictionary.Add(card, cardButton);
            }
            else
            {
                cardButton = deckButtonDictionary[card];
                cardButton.IncrementCount();
            }
        }
        else
        {
            if ( !availableButtonDictionary.ContainsKey(card))
            {
                cardButton = Instantiate( cardButton, availableCards.transform );
                cardButton.Initialize(card, 1, false);
                cardButton.DisableButtons();
                availableButtonDictionary.Add(card, cardButton);
            }
            else
            {
                cardButton = availableButtonDictionary[card];
                cardButton.IncrementCount();
            }

        }
        
    }
    public void ConfirmDeck()
    {
        SessionChoices.Instance.chosenHero = chosenHero;
        SessionChoices.Instance.deck = deck;
        menuManager.ToBattleMenu();
    }
    public void Cancel()
    {
        menuManager.ToBattleMenu();
    }
}
