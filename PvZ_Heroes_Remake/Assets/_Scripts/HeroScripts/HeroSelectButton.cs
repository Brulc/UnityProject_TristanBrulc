using UnityEngine;
using UnityEngine.UI;

public class HeroSelectButton : MonoBehaviour
{
    [SerializeField] private Image heroFace;
    [SerializeField] private Image LeftClassColour;
    [SerializeField] private Image RightClassColour;
    private HeroInfo hero;
    
    public void SetHero ( HeroInfo displayedHero ) { hero = displayedHero; }
    public void OnClick()
    {
        DeckBuilderManager.Instance.chosenHero = hero;
        DeckBuilderManager.Instance.ResetDeckBuilder();
        DeckBuilderManager.Instance.DrawAvailableCards();
    }
    public void Initialize ()
    {
        heroFace.sprite = hero.CardImage;
        
        switch(hero.class1)
        {
            case Class.Beastly:
                LeftClassColour.color = Color.lightBlue;
                break;
            case Class.Brainy:
                LeftClassColour.color = Color.magenta;
                break;
            case Class.Crazy:
                LeftClassColour.color = Color.purple;
                break;
            case Class.Hearty:
                LeftClassColour.color = Color.orange;
                break;
            case Class.Sneaky:
                LeftClassColour.color = Color.black;
                break;
            
            case Class.Guardian:
                LeftClassColour.color = Color.brown;
                break;
            case Class.Solar:
                LeftClassColour.color = Color.yellow;
                break;
            case Class.Smarty:
                LeftClassColour.color = Color.lightGray;
                break;
            case Class.MegaGrow:
                LeftClassColour.color = Color.green;
                break;
            case Class.Kabloom:
                LeftClassColour.color = Color.red;
                break;
        }
        switch(hero.class2)
        {
            case Class.Beastly:
                RightClassColour.color = Color.lightBlue;
                break;
            case Class.Brainy:
                RightClassColour.color = Color.magenta;
                break;
            case Class.Crazy:
                RightClassColour.color = Color.purple;
                break;
            case Class.Hearty:
                RightClassColour.color = Color.orange;
                break;
            case Class.Sneaky:
                RightClassColour.color = Color.black;
                break;
            
            case Class.Guardian:
                RightClassColour.color = Color.brown;
                break;
            case Class.Solar:
                RightClassColour.color = Color.yellow;
                break;
            case Class.Smarty:
                RightClassColour.color = Color.lightGray;
                break;
            case Class.MegaGrow:
                RightClassColour.color = Color.green;
                break;
            case Class.Kabloom:
                RightClassColour.color = Color.red;
                break;
        }
    }
}
