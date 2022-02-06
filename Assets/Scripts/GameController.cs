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
    public GameObject cardPrefab;
    public GameObject enemyZone;

    private Dictionary<int, Card> playerCards;
    private Dictionary<int, Card> enemyCards;

    public List<Card.CardTypes> enemyCardsSetter;  
    public List<int> enemyCardsPositions;   

    void Awake()
    {
        instance = this;

        playerCards = new Dictionary<int, Card>();
        enemyCards = new Dictionary<int, Card>();
        playerHand = hand.GetComponent<PlayerHand>();  

        Transform[] enemyZones = enemyZone.GetComponentsInChildren<Transform>();      

        int i = 0;
        foreach (Card.CardTypes card in enemyCardsSetter)
        {
            Card nCard = new Card(card);
            enemyCards.Add(i, nCard);
            GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, enemyZones[enemyCardsPositions[i]+1]) as GameObject;
            newCard.transform.LeanScale(new Vector2(1.3f, 1.3f), 0.15f);
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
