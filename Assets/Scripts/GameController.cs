using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get{ return instance;} }
    private static PlayerHand playerHand;
    public static PlayerHand PlayerHandInstance { get{ return playerHand; } }
    public GameObject hand;

    private Dictionary<int, Card> playerCards;
    private Dictionary<int, Card> enemyCards;

    public List<Card.CardTypes> enemyCardsSetter;
    public GameObject cardPrefab;
    public GameObject mobZone;

    void Awake()
    {
        instance = this;

        playerCards = new Dictionary<int, Card>();
        enemyCards = new Dictionary<int, Card>();
        playerHand = hand.GetComponent<PlayerHand>();

        int i = 0;
        foreach (Card.CardTypes card in enemyCardsSetter)
        {
            Card nCard = new Card(card);
            enemyCards.Add(i, nCard);
            GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, mobZone.transform) as GameObject;
            CardDragger nCD = newCard.GetComponent<CardDragger>();
            nCD.ID = i;
            nCD.ThisCard = nCard;
            newCard.GetComponentsInChildren<TextMeshProUGUI>()[0].SetText(enemyCards[i].CardStats.Name);
            i++;
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void DropCard(int cardID, Card card)
    {
        playerCards.Add(cardID, card);
    }
}
