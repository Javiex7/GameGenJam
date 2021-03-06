using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform returnParent = null;
    public bool playedCard;
    public bool usedCard = false;
    [SerializeField] private int id;

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    private Card thisCard;

    public Card ThisCard
    {
        get { return thisCard; }
        set { thisCard = value; }
    }

    public enum slotType
    {
        CARD,
        CONDITION,
        RESULT
    }

    public slotType typeOfSlot;

    private Vector2 scale;
    private GameObject placeHolder = null;

    private bool
        ruleSetted; //Esta variable sirve para marcar que una carta no se puede mver porque es regla o carta enemiga

    public bool RuleSetted
    {
        get { return ruleSetted; }
        set { ruleSetted = value; }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>().playSound("Card");

        if (!ruleSetted)
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

            if (typeOfSlot == slotType.CARD && !playedCard)
            {
                scale = new Vector2(2.5f, 2.5f);
            }
            else
            {
                scale = new Vector2(1.6f, 1.6f);
            }
            this.transform.LeanScale(scale, 0.15f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!ruleSetted)
        {
            this.transform.position = eventData.position;

            int newPos = returnParent.childCount;

            if (typeOfSlot == slotType.CARD)
            {
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
                }

                placeHolder.transform.SetSiblingIndex(newPos);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!ruleSetted)
        {
            this.transform.SetParent(returnParent);

            this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
            Destroy(placeHolder);

            GetComponent<CanvasGroup>().blocksRaycasts = true;

            this.transform.LeanScale(Vector2.one, 0.15f);

            if (typeOfSlot == slotType.CARD && this.playedCard)
            {
                this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(205, 270);
            }
        }
    }
}