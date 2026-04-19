using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroDisplay : MonoBehaviour
{
    public Image BodyImage;
    public Player player;
    public TextMeshProUGUI healthText;
    public void Initialize(Player player) 
    {
        this.player = player;
        BodyImage.preserveAspect = true;
        BodyImage.sprite = player.hero.FullImage;
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        healthText.text = (player.health < 0)? "0":player.health.ToString();
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
