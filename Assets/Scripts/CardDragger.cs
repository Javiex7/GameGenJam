using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform returnParent = null;
    public bool playedCard;    
    private int id;
    public int ID{ get{ return id; } set{ id = value; } }
    private Card thisCard;
    public Card ThisCard{ get{ return thisCard; } set{ thisCard = value; } }

    public void OnBeginDrag(PointerEventData eventData)
    {                
        returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(returnParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}