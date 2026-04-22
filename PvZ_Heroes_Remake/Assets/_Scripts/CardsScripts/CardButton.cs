using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardButton : MonoBehaviour
{
    
    private CardInfo card;
    [SerializeField] private Image cardImage;
    [SerializeField] TextMeshProUGUI cardCostText;
    [SerializeField] TextMeshProUGUI cardAttackText;
    [SerializeField] TextMeshProUGUI cardHealthText;
    [SerializeField] Image rarityImage;
    [SerializeField] Image classColorImage;
    [SerializeField] private Image attackBubble;
    [SerializeField] private Image healthBubble;

    [SerializeField] private GameObject addCardButton;
    [SerializeField] private GameObject removeCardButton;
    [SerializeField] private GameObject infoButton;
    private bool isInDeck;

    private int count;
    [SerializeField] TextMeshProUGUI countText;

    public void OnClick()
    {
        if ( !addCardButton.activeInHierarchy )
        {
            if (!isInDeck) { addCardButton.SetActive(true); }
            else { removeCardButton.SetActive(true); }
            infoButton.SetActive(true);
        }
        else
        {
            DisableButtons();
        }
    }
    public void DisableButtons()
    {
        addCardButton.SetActive(false);
        removeCardButton.SetActive(false);
        infoButton.SetActive(false);
    }
    public void AddToDeck()
    {
        Debug.Log("Add to deck");
        DeckBuilderManager.Instance.deck.AddCard(card);
        DeckBuilderUIManager.Instance.DrawCard(card, true);
        //transform.SetParent(deckBuilder.deckCards.transform);
        count--;
        countText.text = count.ToString();
        if (count==0)
        {
            DeckBuilderUIManager.Instance.availableButtonDictionary.Remove(card);
            Destroy(this.gameObject);
        }
    }
    public void RemoveFromDeck()
    {
        Debug.Log("Remove from deck");
        DeckBuilderManager.Instance.deck.RemoveCard(card);
        DeckBuilderUIManager.Instance.DrawCard(card, false);
        //transform.SetParent(deckBuilder.availableCards.transform);
        count--;
        countText.text = count.ToString();
        if (count==0)
        {
            DeckBuilderUIManager.Instance.deckButtonDictionary.Remove(card);
            Destroy(this.gameObject);
        }
    }
    public void ShowInfo()
    {
        DeckBuilderUIManager.Instance.ShowInfoScreen(card);
    }
    public void Initialize(CardInfo cardInfo, int count, bool inDeck)
    {
        card = cardInfo;
        isInDeck = inDeck;
        this.count = count;
        countText.text = count.ToString();
        cardImage.sprite = card.cardImage;
        cardCostText.text = card.cost.ToString();
        
        if ( card.attack == 0 )
        {
            cardAttackText.enabled = false;
            attackBubble.enabled = false;
        }
        else
        {
            cardAttackText.enabled = true;
            attackBubble.enabled = true;
            cardAttackText.text = card.attack.ToString();
        }
        if ( card.health == 0 )
        {
            cardHealthText.enabled = false;
            healthBubble.enabled = false;
        }
        else
        {
            cardHealthText.enabled = true;
            healthBubble.enabled = true;
            cardHealthText.text = card.health.ToString();
        }
        
        switch (card.cardRarity)
        {
            case Rarity.common:
                rarityImage.color = Color.gray;
                break;
            case Rarity.rare:
                rarityImage.color = Color.blue;
                break;
            case Rarity.epic:
                rarityImage.color = Color.darkMagenta;
                break;
            case Rarity.legendary:
                rarityImage.color = Color.gold;
                break;
        }
        switch(card.cardClass)
        {
            case Class.Beastly:
                classColorImage.color = Color.lightBlue;
                break;
            case Class.Brainy:
                classColorImage.color = Color.magenta;
                break;
            case Class.Crazy:
                classColorImage.color = Color.purple;
                break;
            case Class.Hearty:
                classColorImage.color = Color.orange;
                break;
            case Class.Sneaky:
                classColorImage.color = Color.black;
                break;
            
            case Class.Guardian:
                classColorImage.color = Color.brown;
                break;
            case Class.Solar:
                classColorImage.color = Color.yellow;
                break;
            case Class.Smarty:
                classColorImage.color = Color.lightGray;
                break;
            case Class.MegaGrow:
                classColorImage.color = Color.green;
                break;
            case Class.Kabloom:
                classColorImage.color = Color.red;
                break;
        }
    }
    public void IncrementCount () { count++; countText.text = count.ToString(); }
    public void DecrementCount() { count--; countText.text = count.ToString(); }
}
