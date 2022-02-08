using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get{ return instance;} }
    private static PlayerHand playerHand;
    public static PlayerHand PlayerHandInstance { get{ return playerHand; } }
    public GameObject hand;
    public GameObject cardPrefab;
    public GameObject enemyZone, playerZone;
    public GameObject attackEnemyUI, cloneCardUI;
    public Transform[] enemyZones;
    public Transform[] playerZones;
    public int rule1, rule2, rule3;

    private Dictionary<int, Card> playerCards;
    private Dictionary<int, Card> enemyCards;

    public List<Card.CardTypes> enemyCardsSetter;  
    public List<int> enemyCardsPositions;   

    public int playerMana;
    public int playerHP;
    private int rounds;

    private GameObject lastCardClicked;
    private bool selectCard;


    void Awake()
    {
        instance = this;

        rounds = 0;
        lastCardClicked = null;
        selectCard = false;

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
            newCard.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(205, 270);  
            CardDragger nCD = newCard.GetComponent<CardDragger>();
            nCD.RuleSetted = true;
            nCD.ID = cID;
            nCD.ThisCard = nCard;          
            i++;
        }        
    }
    
    void Start()
    {          
        UICard.instance.SetEnemyUI();
        attackEnemyUI.SetActive(false);
        cloneCardUI.SetActive(false); 
        StartCoroutine("FirstFrame");       
    }

    IEnumerator FirstFrame()
    {
        yield return new WaitForSeconds(0.25f);
        ExecuteStartingCondition();
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
                    ExecutePlushDeathCondition();
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
                    ExecutePlushDeathCondition();
                    break;
                }
            }
        } 

        CheckRoundsEnded();  
        CheckNumberPlayerCards();  
        CheckVictory();              
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
            UICard.instance.SetEnemyHPUI();
            ExecutePlayerAttackCondition();
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

    public void NextRound()
    {

        foreach(Transform t in playerZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card != null)
                card.usedCard = true;
        }
        
        //Enemies attack
        for(int i = 1; i < playerZones.Length; i++)
        {
            CardDragger eCard = enemyZones[i].gameObject.GetComponentInChildren<CardDragger>();
            CardDragger pCard = playerZones[i].gameObject.GetComponentInChildren<CardDragger>();

            if(eCard != null)
            {
                if(pCard != null)
                {
                    EnemyAttack(eCard.ID, pCard.ID);
                }
                else
                {
                    ReducePlayerHP(eCard.ThisCard.CardStats.Attack);
                    ExecutePlayerDamageCondition();
                }
            }
        }

        foreach(Transform t in playerZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card != null)
                card.usedCard = false;
        }

        rounds++;
        ExecuteStartingCondition();
    }

    public void EnemyAttack(int attackerID, int attackedID)
    {        
        Card attacker, attacked;

        if(enemyCards.TryGetValue(attackerID, out attacker) && playerCards.TryGetValue(attackedID, out attacked))
        {
            attacked.ReduceHP(attacker.CardStats.Attack);
            UICard.instance.SetPlayerHPUI();
        }        
    }  

    public void ReducePlayerHP(int pointsLost){

        if(playerHP == 0 || playerHP <= pointsLost)
            playerHP = 0;
        else
            playerHP -= pointsLost;
    }  

    public void Damage2Card(int attackedID)
    {        
        Card attacked;

        if(enemyCards.TryGetValue(attackedID, out attacked))
        {
            attacked.ReduceHP(2);
            UICard.instance.SetEnemyHPUI();
        }        
    }

    public void ClickCard()
    {
        if(selectCard)
            lastCardClicked = EventSystem.current.currentSelectedGameObject;
        else
            lastCardClicked = null;
    }

    IEnumerator SelectEnemyCard()
    {
        attackEnemyUI.SetActive(true);
        lastCardClicked = null;
        selectCard = true;
        while(selectCard)
        {
            if(lastCardClicked != null)
            {
                CardDragger card = lastCardClicked.GetComponentInChildren<CardDragger>();
                if(card != null)
                {
                    Damage2Card(card.ID);
                    selectCard = false;
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
        attackEnemyUI.SetActive(false);
    }

    IEnumerator SelectMyCard()
    {
        if(playerCards.Count != 0)
        {
            cloneCardUI.SetActive(true);
            lastCardClicked = null;
            selectCard = true;
            while(selectCard)
            {
                if(lastCardClicked != null)
                {
                    CardDragger card = lastCardClicked.GetComponentInChildren<CardDragger>();
                    if(card != null)
                    {
                        hand.GetComponent<PlayerHand>().AddCard(card.gameObject, card.ThisCard.myType);
                        selectCard = false;
                    }
                }
                yield return new WaitForSeconds(0.25f);
            }
            cloneCardUI.SetActive(false);
        }        
    }

    IEnumerator SpawnRabbit()
    {
        CardDragger card = null;
        int pos = 0;        
        yield return new WaitForSeconds(0.25f);
        for(int i = 1; i < playerZones.Length; i++)
        {
            card = playerZones[i].gameObject.GetComponentInChildren<CardDragger>();

            if(card == null)
            { 
                pos = i;      
                i = playerZones.Length;
            }
        }   

        int cID = hand.GetComponent<PlayerHand>().lastID;
        Card nCard = new Card(Card.CardTypes.Conejo, cID);

        playerCards.Add(cID, nCard);
        GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, playerZones[pos]) as GameObject; 

        newCard.GetComponent<Image>().sprite = UICard.instance.Conejo;  

        newCard.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(205, 270);  
        CardDragger nCD = newCard.GetComponent<CardDragger>();
        nCD.playedCard = true;
        nCD.ID = cID;
        nCD.ThisCard = nCard;

        hand.GetComponent<PlayerHand>().lastID++;        
    }

    private void ExchangeAttackHP()
    {
        foreach(Card card in playerCards.Values){
            int hp = card.CurrentHP;
            card.CurrentHP = card.CardStats.Attack;
            card.CardStats.Attack = hp;
        }

        foreach(Card card in enemyCards.Values){
            int hp = card.CurrentHP;
            card.CurrentHP = card.CardStats.Attack;
            card.CardStats.Attack = hp;
        }

        foreach(Card card in playerHand.playerHand.Values){
            int hp = card.CurrentHP;
            card.CurrentHP = card.CardStats.Attack;
            card.CardStats.Attack = hp;
        }

        UICard.instance.SetAttackHPUI();
    }

    public void Damage1AllCard()
    {        
        foreach(Card card in playerCards.Values){
            card.CurrentHP--;
        }

        foreach(Card card in enemyCards.Values){
            card.CurrentHP--;
        } 

        UICard.instance.SetEnemyHPUI();
        UICard.instance.SetPlayerHPUI();       
    }

    private void ExecuteStartingCondition()
    {
        switch(rule1)
        {
            case 0:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 1:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 2:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 3:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 4:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }

        switch(rule2)
        {
            case 0:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 1:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 2:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 3:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 4:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }

        switch(rule3)
        {
            case 0:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 1:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 2:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 3:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 4:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }
    }

    public void ExecuteCardPlayedCondition()
    {
        switch(rule1)
        {
            case 10:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 11:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 12:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 13:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 14:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }

        switch(rule2)
        {
            case 10:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 11:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 12:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 13:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 14:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }

        switch(rule3)
        {
            case 10:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 11:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 12:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 13:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 14:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }
    }

    private void ExecutePlushDeathCondition()
    {
        switch(rule1)
        {
            case 20:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 21:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 22:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 23:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 24:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }

        switch(rule2)
        {
            case 20:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 21:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 22:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 23:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 24:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }

        switch(rule3)
        {
            case 20:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 21:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 22:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 23:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 24:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }
    }

    private void ExecutePlayerAttackCondition()
    {
        switch(rule1)
        {
            case 30:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 31:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 32:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 33:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 34:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }

        switch(rule2)
        {
            case 30:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 31:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 32:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 33:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 34:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }

        switch(rule3)
        {
            case 30:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 31:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 32:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 33:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 34:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }
    }

    private void ExecutePlayerDamageCondition()
    {        
        switch(rule1)
        {
            case 40:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 41:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 42:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 43:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 44:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }

        switch(rule2)
        {
            case 40:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 41:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 42:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 43:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 44:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }

        switch(rule3)
        {
            case 40:
                //Un peluche recibe 2 de daño
                StartCoroutine("SelectEnemyCard");
            return;

            case 41:
                //Spawnea un conejo 1/1
                StartCoroutine("SpawnRabbit");
            return;

            case 42:
                //Intercambia ataque y vida
                ExchangeAttackHP();
            return;

            case 43:
                //Todos los peluches reciben 1 de daño
                Damage1AllCard();
            return;

            case 44:
                //Crear copia peluche
                StartCoroutine("SelectMyCard");
            return;

            default:
            break;
        }
    }

    public void CheckRoundsEnded()
    {
        string nameS = SceneManager.GetActiveScene().name;
        if(nameS.Equals("Level1"))
        {
            if(rounds >= 2)
            {
                Win_Lose.instance.setLose();
            }
        }
        else if(nameS.Equals("Level2"))
        {
            if(rounds >= 1)
            {
                Win_Lose.instance.setLose();
            }
        }
    }

    public void CheckNumberPlayerCards()
    {
        if(playerCards.Count == 0 && hand.GetComponent<PlayerHand>().playerHand.Count == 0){
            Win_Lose.instance.setLose();
        }
    }

    public void CheckVictory()
    {
        string nameS = SceneManager.GetActiveScene().name;
        if(nameS.Equals("Level1"))
        {
            if(enemyZones[3].gameObject.GetComponentInChildren<CardDragger>() == null)
            {
                Win_Lose.instance.setWin();
            }
        }
        else if(nameS.Equals("Level2"))
        {
            bool noOne = true;

            foreach(Transform t in enemyZones)
            {
                if(t.gameObject.GetComponentInChildren<CardDragger>() != null)
                {
                    noOne = false;
                }
            }

            foreach(Transform t in playerZones)
            {
                if(t.gameObject.GetComponentInChildren<CardDragger>() != null)
                {
                    noOne = false;
                }
            }

            if(noOne)
            {
                Win_Lose.instance.setWin();
            }
        }
        else if(nameS.Equals("Level3"))
        {
            if(rounds >= 2)
            {
                Win_Lose.instance.setWin();
            }
        }
    }
}
