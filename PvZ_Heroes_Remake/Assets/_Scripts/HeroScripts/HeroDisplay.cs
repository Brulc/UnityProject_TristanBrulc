using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class HeroDisplay : MonoBehaviour, IDropHandler
{
    public Image BodyImage;
    public Player player;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    [SerializeField] private Image trickTarget;
    public void Initialize(Player player) 
    {
        this.player = player;
        BodyImage.preserveAspect = true;
        BodyImage.sprite = player.hero.FullImage;
        UpdateMana();
        UpdateHealth();
        trickTarget.enabled = false;
    }
    public void UpdateHealth()
    {
        healthText.text = (player.health < 0)? "0":player.health.ToString();
    }
    public void UpdateMana()
    {
        manaText.text = player.mana.ToString();
    }
    public void ShowTrickTarget() { trickTarget.enabled = true; Debug.Log("show me yo willy"); }
    public void HideTrickTarget() { trickTarget.enabled = false; }
    public void OnDrop(PointerEventData eventData)
    {
        BoardManager.Instance.OnPlayedAbility(eventData, player);
    }
    public void TakeDamage()
    {
        BodyImage.color = Color.red;
        UpdateHealth();
    }
    public void Heal()
    {
        BodyImage.color = Color.limeGreen;
        UpdateHealth();
    }
}
