using UnityEngine;
using UnityEngine.UI;

public class BattleMenuManager : MonoBehaviour
{
    [SerializeField] private Image deckButtonImage;
    void Start()
    {
        DrawDeckButton();
    }
    public void ChooseManTeam() { SessionChoices.Instance.ChooseManTeam(); }
    public void ChooseWomanTeam() { SessionChoices.Instance.ChooseWomanTeam(); }
    public void GoToDeckBuilder() { MenuManager.Instance.DeckBuilder(); }
    public void DrawDeckButton()
    {
        deckButtonImage.sprite = SessionChoices.Instance.chosenHero.CardImage;
    }
    public void BotBattle()
    {
        MenuManager.Instance.StartGame();
    }
    public void PVPBattle()
    {
        
    }
}
