using UnityEngine;
using UnityEngine.EventSystems;

public class CardMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private CanvasGroup canvasGroup;
    
    Transform parentAfterDrag;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject dragged = eventData.pointerDrag;
        CardInfo draggedCard = dragged.GetComponent<CardDisplay>().card;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;

        var playability = GameManager.Instance.GetCurrentPlayability(draggedCard); //minion, trick

        if ( draggedCard.cost <= ((draggedCard.cardTeam == Team.Man)? BoardManager.Instance.manPlayer.mana:BoardManager.Instance.womanPlayer.mana) )
        {
            Debug.Log(playability[0].ToString() + playability[1].ToString());
            if ( playability[0] )
            {
                UIManager.Instance.ShowDropBoxes(draggedCard.Amphibious);
            }
            else if ( playability[1] )
            {
                Debug.Log("show the dam targets");
                UIManager.Instance.ShowTrickTarget( (draggedCard.cardTeam == Team.Man)? Team.Woman: Team.Man );
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
    
        canvasGroup.blocksRaycasts = true;
        UIManager.Instance.HideDropBoxes();
        UIManager.Instance.HideTrickTarget();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
        UIManager.Instance.ShowInfoScreen(eventData);
    }
}

