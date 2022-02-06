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
        if (typeOfSlot == cd.typeOfSlot)
        {
            if (cd.typeOfSlot == CardDragger.slotType.CARD)
            {
                // Check if the card has been played early if not, position the card in the slot
                if (cd.playedCard == false && oneCard == false)
                {
                    if (initialDeck == false)
                    {
                        GameController.PlayerHandInstance.DragCard(cd.ID);
                        oneCard = true;
                        cd.playedCard = true;
                    }
                    cd.returnParent = this.transform;
                    GameController.Instance.DropCard(cd.ID, cd.ThisCard);
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