using UnityEngine;
using UnityEngine.EventSystems;

public class CardMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
            GameObject dropped = eventData.pointerDrag;
            CardInfo droppedCard = dropped.GetComponent<CardDisplay>().card;
            if ( droppedCard.type == CardType.Minion )
                UIManager.Instance.ShowDropBoxes();
            else
                UIManager.Instance.ShowTrickTarget( (droppedCard.cardTeam == Team.Man)? Team.Woman: Team.Man );
            
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
}

