using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeckBuilderUIManager : MonoBehaviour
{
    public static DeckBuilderUIManager Instance;

    #region References
    [SerializeField] private HeroSelectButton referenceHeroButton;
    [SerializeField] private CardButton referenceCardButton;
    #endregion

    #region Card Buttons
    private CardButton cardButton;
    private List<CardButton> cardButtons;
    // ni isti reference sam koga jebe
    [SerializeField] private GameObject cardDescription;
    #endregion

    #region Parents
    [SerializeField] private GameObject heroButtonsParent;
    [SerializeField] private GameObject availableCards;
    [SerializeField] private GameObject deckCards;
    #endregion

    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    [HideInInspector] public Dictionary<CardInfo, CardButton> deckButtonDictionary;
    [HideInInspector] public Dictionary<CardInfo, CardButton> availableButtonDictionary;

    void Awake ()
    {
        Instance = this;
        
        deckButtonDictionary = new();
        availableButtonDictionary = new();
        cardButtons = new();
        cardButton = referenceCardButton;
        
    }
    public void EraseAllCards()
    {
        foreach ( var button in cardButtons ) 
        { 
            Destroy(button.gameObject); 
        }
        deckButtonDictionary = new();
        availableButtonDictionary = new();
        cardButtons = new();
    }
    public void DrawStartCards( CardInfo card, int count )
    {
        cardButton = Instantiate( referenceCardButton, availableCards.transform );
        cardButton.Initialize( card, count, false );
        cardButtons.Add(cardButton);
        availableButtonDictionary.Add(card, cardButton);
    }
    public void DrawCard(CardInfo card, bool addCard)
    {
        if (addCard)
        {
            if ( !deckButtonDictionary.ContainsKey(card))
            {
                cardButton = Instantiate( referenceCardButton, deckCards.transform );
                cardButton.Initialize(card, 1, true);
                cardButton.DisableButtons();
                deckButtonDictionary.Add(card, cardButton);
                cardButtons.Add(cardButton);
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
                cardButton = Instantiate( referenceCardButton, availableCards.transform );
                cardButton.Initialize(card, 1, false);
                cardButton.DisableButtons();
                availableButtonDictionary.Add(card, cardButton);
                cardButtons.Add(cardButton);
            }
            else
            {
                cardButton = availableButtonDictionary[card];
                cardButton.IncrementCount();
                cardButtons.Add(cardButton);
            }

        }
    }
    public void DrawHeroButton(HeroInfo hero)
    {
        HeroSelectButton newButton = Instantiate(referenceHeroButton, heroButtonsParent.transform);
        newButton.SetHero(hero);
        newButton.Initialize();
    }
    
    public void ShowInfoScreen(CardInfo card)
    {
        Debug.Log("IShowInfo");
        cardDescription.SetActive(true);
        cardDescription.GetComponent<CardDescription>().Show(card);
    }
    public void HideInfoScreen()
    {
        cardDescription.SetActive(false);
    }
}
