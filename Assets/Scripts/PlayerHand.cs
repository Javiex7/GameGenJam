using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public List<Card.CardTypes> handCreator;
    
    private Dictionary<int, Card> playerHand;

    void Awake()
    {
        playerHand = new Dictionary<int, Card>();

        int i = 0;
        foreach (Card.CardTypes card in handCreator)
        {
            playerHand.Add(i, new Card (card));
            i++;
        }
    }

    void Start()
    {
        foreach (int card in playerHand.Keys)
        {
            Debug.Log(card+": " + playerHand[card].CardStats.Name);
        }
    }

    void Update()
    {
        
    }

    public Card DragCard(int pos)
    {
        Card draggedCard;
        if(playerHand.TryGetValue(pos, out draggedCard))
            return draggedCard;
        else
            return null;
    }
}
