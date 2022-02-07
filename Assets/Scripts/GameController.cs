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
    public Transform[] playerZones;

    private Dictionary<int, Card> playerCards;
    private Dictionary<int, Card> enemyCards;

    public List<Card.CardTypes> enemyCardsSetter;  
    public List<int> enemyCardsPositions;   

    public int playerMana;
    private int rounds;
    public bool roundEnded;

    void Awake()
    {
        instance = this;

        rounds = 0;
        roundEnded = false;

        playerCards = new Dictionary<int, Card>();
        enemyCards = new Dictionary<int, Card>();
        playerHand = hand.GetComponent<PlayerHand>();  

        enemyZones = enemyZone.GetComponentsInChildren<Transform>();    
        playerZones = playerZone.GetComponentsInChildren<Transform>();

        int i = 0;
        foreach (Card.CardTypes card in enemyCardsSetter)
        {            
            int cID = enemyCardsPositions[i]+1;
            Card nCard = new Card(card, cID);
            enemyCards.Add(cID, nCard);
            GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, enemyZones[cID]) as GameObject;   
            newCard.transform.LeanScale(new Vector2(1.3f, 1.3f), 0.15f);  
            CardDragger nCD = newCard.GetComponent<CardDragger>();
            nCD.RuleSetted = true;
            nCD.ID = cID;
            nCD.ThisCard = nCard;          
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
        
        if(roundEnded)
        {
            //ATACAN ENEMIGOS

            foreach(Transform t in playerZones)
            {
                CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
                if(card != null)
                    card.usedCard = false;
            }

            rounds++;
            roundEnded = false;
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
            attacked.ReduceHP(attacker.CardStats.Attack);
        }        
    }

    private void DeletePlayerCard(int cardID)
    {       
        foreach(Transform t in playerZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card != null && card.ID == cardID)
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
            if(card != null && card.ID == cardID)
            {
                Destroy(card.gameObject);    
                enemyCards.Remove(cardID);            
                break;
            }
        }        
    }

    public void ReduceMana(int cost)
    {
        playerMana -= cost;      
    }

    public bool AbleToUseMana(int cost)
    {
        if(cost > playerMana)
            return false;
        else 
            return true;
    }

    public bool CheckIfRoundEnded()
    {
        int nullChilds = 0;
        foreach(Transform t in playerZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card != null && card.usedCard == false)
            {
                return false;
            } 
            else if(card == null)
            {
                nullChilds++;
            }
        }

        if(nullChilds == 6)
            return false; 
        else
            return true;
    }
}
