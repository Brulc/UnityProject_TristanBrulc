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
        if ( BoardManager.Instance.CheckPlayability())
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            canvasGroup.blocksRaycasts = false;
            GameObject dragged = eventData.pointerDrag;
            CardInfo draggedCard = dragged.GetComponent<CardDisplay>().card;
            if ( draggedCard.type == CardType.Minion )
                UIManager.Instance.ShowDropBoxes();
            else if ( draggedCard.type == CardType.Trick ) 
            {

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
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}

