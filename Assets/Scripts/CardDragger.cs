using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform returnParent = null;
    public bool playedCard;

    public enum slotType
    {
        CARD,
        CONDITION,
        RESULT
    }

    public slotType typeOfSlot = slotType.CARD;

    private Vector2 scale = new Vector2(1.4f, 1.4f);
    private GameObject placeHolder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());


        returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        this.transform.LeanScale(scale, 0.15f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;


        int newPos = returnParent.GetChildCount();

        for (int i = 0; i < returnParent.childCount; i++)
        {
            if (this.transform.position.x < returnParent.GetChild(i).position.x)
            {
                newPos = i;
                if (placeHolder.transform.GetSiblingIndex() < newPos)
                {
                    newPos--;
                }

                break;
            }

            placeHolder.transform.SetSiblingIndex(newPos);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(returnParent);

        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        Destroy(placeHolder);


        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (playedCard == true)
        {
            this.transform.LeanScale(new Vector2(1.3f, 1.3f), 0.15f);
        }
        else
        {
            this.transform.LeanScale(Vector2.one, 0.15f);
        }
    }
}