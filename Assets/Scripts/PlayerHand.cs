using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHand : MonoBehaviour
{
    [Tooltip("Choose a number between 1 and 8")]
    public List<Card.CardTypes> handCreator;
    public GameObject cardPrefab;
    
    private Dictionary<int, Card> playerHand;

    void Awake()
    {
        playerHand = new Dictionary<int, Card>();

        int i = 0;
        foreach (Card.CardTypes card in handCreator)
        {
            Card nCard = new Card(card, i);
            playerHand.Add(i, nCard);
            GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, gameObject.transform) as GameObject;
            newCard.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(190, 250);
            CardDragger nCD = newCard.GetComponent<CardDragger>();
            nCD.RuleSetted = false;
            nCD.ID = i;
            nCD.ThisCard = nCard;
            i++;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void DragCard(int cardID)
    {
        if(playerHand.ContainsKey(cardID))
        {
            playerHand.Remove(cardID);
        }
    }
}
