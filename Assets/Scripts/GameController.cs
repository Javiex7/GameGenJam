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
    public GameObject enemyZone, playerZone;
    public Transform[] enemyZones;

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

        enemyZones = enemyZone.GetComponentsInChildren<Transform>();      

        int i = 0;
        foreach (Card.CardTypes card in enemyCardsSetter)
        {            
            int cID = enemyCardsPositions[i]+1;
            Card nCard = new Card(card, cID);
            enemyCards.Add(cID, nCard);
            GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, enemyZones[cID]) as GameObject;
            newCard.transform.LeanScale(new Vector2(1.3f, 1.3f), 0.15f);
            CardDragger nCD = newCard.GetComponent<CardDragger>();
            nCD.ID = cID;
            nCD.ThisCard = nCard;
            newCard.GetComponentsInChildren<TextMeshProUGUI>()[0].SetText(enemyCards[cID].CardStats.Name);
            i++;
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        foreach(int pCardID in playerCards.Keys)
        {
            Card pCard;
            if(playerCards.TryGetValue(pCardID, out pCard))
            {
                if(pCard.CurrentHP == 0)
                {
                    DeletePlayerCard(pCard.MyID);
                    break;
                }
            }
        }

        foreach(int eCardID in enemyCards.Keys)
        {
            Card eCard;
            if(enemyCards.TryGetValue(eCardID, out eCard))
            {
                if(eCard.CurrentHP == 0)
                {
                    DeleteEnemyCard(eCard.MyID);
                    break;
                }
            }
        }
    }

    public void DropCard(int cardID, Card card)
    {
        playerCards.Add(cardID, card);
    }

    public void PlayerAttack(int attackerID, int attackedID)
    {        
        Card attacker, attacked;

        if(playerCards.TryGetValue(attackerID, out attacker) && enemyCards.TryGetValue(attackedID, out attacked))
        {
            Debug.Log("Ataque de " + attacker.CardStats.Name + " a " + attacked.CardStats.Name);
            Debug.Log("HP de "+attacked.CardStats.Name + ": " + attacked.CurrentHP);

            attacked.ReduceHP(attacker.CardStats.Attack);

            Debug.Log(attacked.CardStats.Name + " recibe " + attacker.CardStats.Attack+ " puntos de daño");
            Debug.Log("HP de "+attacked.CardStats.Name + ": " + attacked.CurrentHP);
        }        
    }

    private void DeletePlayerCard(int cardID)
    {
        Transform[] playerZones = playerZone.GetComponentsInChildren<Transform>();
        foreach(Transform t in playerZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card.ID == cardID)
            {
                Destroy(card.gameObject); 
                playerCards.Remove(cardID);               
                break;
            }
        }        
    }

    private void DeleteEnemyCard(int cardID)
    {
        foreach(Transform t in enemyZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card.ID == cardID)
            {
                Destroy(card.gameObject);    
                enemyCards.Remove(cardID);            
                break;
            }
        }        
    }
}
