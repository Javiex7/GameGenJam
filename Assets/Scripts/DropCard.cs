using UnityEngine;
using UnityEngine.EventSystems;

public class DropCard : MonoBehaviour, IDropHandler
{
    private CardDragger actualCard = null;

    public void OnDrop(PointerEventData eventData)
    {
        CardDragger cd = eventData.pointerDrag.GetComponent<CardDragger>();
        if (cd != null && cd.playedCard == false)
        {
            cd.returnParent = this.transform;
            cd.playedCard = true;
        }
        else if (cd.playedCard == true)
        {
        }
    }
}