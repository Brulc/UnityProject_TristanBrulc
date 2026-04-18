using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LaneDisplay : MonoBehaviour, IDropHandler
{
    public LaneInfo laneInfo;
    [SerializeField] private Image laneImage;
    public Image dropBox;
    public GameObject enemyDropBox;

    public void Initialize(LaneInfo laneInfo)
    {
        HideDropBox();
        this.laneInfo = laneInfo;
        switch ( laneInfo.type )
        {
            case LaneType.Heights:
                laneImage.color = Color.brown;
                break;
            case LaneType.Ground:
                laneImage.color = Color.darkGreen;
                break;
            case LaneType.Water:
                laneImage.color = Color.lightBlue;
                break;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if ( eventData.pointerDrag != null )
        {
            
            BoardManager.Instance.SpawnMinionEvent(eventData, this.laneInfo);
            
        }
    }
    public void ShowDropBox()
    {
        dropBox.enabled = true;
    }
    public void HideDropBox()
    {
        dropBox.enabled = false;
    }
}
