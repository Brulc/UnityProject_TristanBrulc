using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinionDisplay : MonoBehaviour
{
    // a bom info tuki zapisu al pa posebi
    [HideInInspector] public MinionInfo minion;
    [SerializeField] private Image minionImage;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private Image attackBubble;
    [SerializeField] private Image trickTarget;
    
    public void Initialize ( MinionInfo minion )
    {
        this.minion = minion;
        minionImage.sprite = minion.card.cardImage;
    
        DisplayCard(true);
    }
    private void DisplayCard(bool initialisation)
    {
        healthText.text = minion.health.ToString();
        if ( minion.attack == 0 && initialisation )
        {
            attackBubble.enabled = false;
            attackText.enabled = false;
        }
        else
        {
            if ( attackBubble.enabled == false )
            {
                attackBubble.enabled = true;
                attackText.enabled = true;
            }
            
            attackText.text = minion.attack.ToString();
        }
    }
    public void ShowTrickTarget() { trickTarget.enabled = true; }
    public void HideTrickTarget() { trickTarget.enabled = false; }
    public void TakeDamage()
    {
        minionImage.color = Color.red;
        UpdateMinion();
    }
    public void Heal()
    {
        minionImage.color = Color.limeGreen;
        UpdateMinion();
    }
    private void UpdateMinion()
    {
        attackText.text = minion.attack.ToString();
        healthText.text = minion.health.ToString();
    }
}
