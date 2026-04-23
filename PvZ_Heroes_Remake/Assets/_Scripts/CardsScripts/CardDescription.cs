using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CardDescription : MonoBehaviour, IPointerClickHandler
{
    public Image cardImage;
    public TextMeshProUGUI cardCostText;
    public TextMeshProUGUI cardAttackText;
    public TextMeshProUGUI cardHealthText;
    [SerializeField] private Image attackBubble;
    [SerializeField] private Image healthBubble;

    [SerializeField] private TextMeshProUGUI nameOfCard;
    [SerializeField] private TextMeshProUGUI tribesOfCard;
    [SerializeField] private TextMeshProUGUI descriptionOfCard;
    
    public void Show(CardInfo card)
    {
        StringBuilder tmpString;

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

        nameOfCard.text = card.cardName;

        tmpString = new ();
        foreach ( Tribe tribe in card.tribes )
        {
            tmpString.Append(tribe.ToString() + " ");
        }
        tmpString.Append(card.type.ToString() + "\n");
        tribesOfCard.text = tmpString.ToString();

        tmpString = new();
        foreach ( var ability in card.abilities )
        {
            tmpString.Append(ability.abilityType.ToString().Replace("_"," ") + ": " + ability.description + "\n");
        }
        descriptionOfCard.text = tmpString.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if ( UIManager.Instance != null )
            UIManager.Instance.HideInfoScreen();
        else if ( DeckBuilderUIManager.Instance != null )
            DeckBuilderUIManager.Instance.HideInfoScreen();
    }
}
