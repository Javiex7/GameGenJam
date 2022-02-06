using UnityEngine;
using UnityEngine.EventSystems;

public class DropCard : MonoBehaviour, IDropHandler
{
    private bool oneCard = false;
    private CardDragger actualCard = null;

    [SerializeField] private bool initialDeck = false;
    public CardDragger.slotType typeOfSlot = CardDragger.slotType.CARD;

    public void OnDrop(PointerEventData eventData)
    {
        CardDragger cd = eventData.pointerDrag.GetComponent<CardDragger>();
        if (cd != null && cd.playedCard == false)
        {
            GameController.PlayerHandInstance.DragCard(cd.ID);
            cd.returnParent = this.transform;
            cd.playedCard = true;              
            GameController.Instance.DropCard(cd.ID, cd.ThisCard);          
        }

        if (typeOfSlot == cd.typeOfSlot)
        {
            if (cd.typeOfSlot == CardDragger.slotType.CARD)
            {
                // Check if the card has been played early if not, position the card in the slot
                if (cd.playedCard == false && oneCard == false)
                {
                    if (initialDeck == false)
                    {
                        oneCard = true;
                        cd.playedCard = true;
                    }
                    cd.returnParent = this.transform;
                }
                else if (cd.playedCard == true)
                {

                }
            }
            else if (cd.typeOfSlot == CardDragger.slotType.CONDITION || cd.typeOfSlot == CardDragger.slotType.RESULT)
            {
                // Check others types of cards (Conditions and Results)

                if (oneCard == false)
                {
                    if (initialDeck == false)
                    {
                        oneCard = true;
                    }
                    cd.returnParent = this.transform;
                }
            }
        }
    }
}