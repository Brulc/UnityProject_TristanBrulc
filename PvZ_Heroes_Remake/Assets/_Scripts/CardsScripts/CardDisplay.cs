using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    // možno boljše nardit info zapisan tukile kokr tm kr info je sam pac kaka je karta na sploh in ne specifična
    public CardInfo card;
    public Image cardImage;
    public TextMeshProUGUI cardCostText;
    public TextMeshProUGUI cardAttackText;
    public TextMeshProUGUI cardHealthText;
    [SerializeField] private Image attackBubble;
    [SerializeField] private Image healthBubble;
    public Image rarityImage;
    public Image classColorImage;

    public void Initialize(CardInfo cardInfo)
    {
        card = cardInfo;
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
}
