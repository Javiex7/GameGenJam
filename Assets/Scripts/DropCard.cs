using UnityEngine;
using UnityEngine.EventSystems;

public class DropCard : MonoBehaviour, IDropHandler
{
    [SerializeField] private bool initialDeck = false;
    public CardDragger.slotType typeOfSlot = CardDragger.slotType.CARD;

    public int cardCount;
    public bool isEnemySlot = false;
    public CardDragger actualCard = null;

    private bool oneCard = false;

    public void OnDrop(PointerEventData eventData)
    {
        CardDragger cd = eventData.pointerDrag.GetComponent<CardDragger>();
        if (typeOfSlot == cd.typeOfSlot)
        {
            if (cd.typeOfSlot == CardDragger.slotType.CARD)
            {
                if(isEnemySlot)
                {
                    if (cd.playedCard == true && transform.childCount > 0)
                    {                        
                        GameController.Instance.PlayerAttack(cd.ID, gameObject.GetComponentInChildren<CardDragger>().ID);  
                    }
                }
                else
                {
                    // Check if the card has been played early if not, position the card in the slot
                    if (cd.playedCard == false && oneCard == false)
                    {
                        if (initialDeck == false)
                        {
                            GameController.PlayerHandInstance.DragCard(cd.ID);
                            oneCard = true;
                            cd.playedCard = true;
                            GameController.Instance.DropCard(cd.ID, cd.ThisCard);
                        }

                        cd.returnParent = this.transform;
                    }
                }                
            }
            else if (cd.typeOfSlot == CardDragger.slotType.CONDITION || cd.typeOfSlot == CardDragger.slotType.RESULT)
            {
                cardCount = this.transform.childCount;
                // Check others types of cards (Conditions and Results)
                if (cardCount < 1 || initialDeck == true)
                {
                    actualCard = cd;
                    cd.returnParent = this.transform;
                }
            }
        }
    }
}