using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    public static UICard instance;
    public GameObject enemyZone, playerZone, pHand;

    public Transform[] enemyZones;
    public Transform[] playerZones;
    public CardDragger[] handCards;

    public Sprite Elefante, Tortuga, Raton, Tigre, Conejo, Abeja, Oso, Serpiente, Cocodrilo, Cangrejo, Tiburon, Pulpo;

    void Awake() {
        instance = this;
        enemyZones = enemyZone.GetComponentsInChildren<Transform>();    
        playerZones = playerZone.GetComponentsInChildren<Transform>();        
    }

    void Start() {
        handCards = pHand.GetComponentsInChildren<CardDragger>();
    }

    public void SetEnemyUI()
    {
        foreach(Transform t in enemyZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card != null)
            {
                Text[] texts = card.gameObject.GetComponentsInChildren<Text>();
                texts[0].text = card.ThisCard.CardStats.Attack.ToString();
                texts[1].text = card.ThisCard.CurrentHP.ToString();

                switch(card.ThisCard.myType)
                {
                    case Card.CardTypes.Pulpo: default:
                        card.gameObject.GetComponent<Image>().sprite = Pulpo;
                    break;
                    case Card.CardTypes.Elefante:
                        card.gameObject.GetComponent<Image>().sprite = Elefante;
                    break;
                    case Card.CardTypes.Tortuga:
                        card.gameObject.GetComponent<Image>().sprite = Tortuga;
                    break;
                    case Card.CardTypes.Raton:
                        card.gameObject.GetComponent<Image>().sprite = Raton;
                    break;
                    case Card.CardTypes.Tigre:
                        card.gameObject.GetComponent<Image>().sprite = Tigre;
                    break;
                    case Card.CardTypes.Conejo:
                        card.gameObject.GetComponent<Image>().sprite = Conejo;
                    break;
                    case Card.CardTypes.Abeja:
                        card.gameObject.GetComponent<Image>().sprite = Abeja;
                    break;
                    case Card.CardTypes.Oso:
                        card.gameObject.GetComponent<Image>().sprite = Oso;
                    break;
                    case Card.CardTypes.Serpiente:
                        card.gameObject.GetComponent<Image>().sprite = Serpiente;
                    break;
                    case Card.CardTypes.Cangrejo:
                        card.gameObject.GetComponent<Image>().sprite = Cangrejo;
                    break;
                    case Card.CardTypes.Tiburon:
                        card.gameObject.GetComponent<Image>().sprite = Tiburon;
                    break;
                }
            }
        } 
    }

    public void SetHandUI()
    {
        foreach(CardDragger card in handCards)
        {
            if(card != null)
            {
                Text[] texts = card.gameObject.GetComponentsInChildren<Text>();
                texts[0].text = card.ThisCard.CardStats.Attack.ToString();
                texts[1].text = card.ThisCard.CurrentHP.ToString();

                switch(card.ThisCard.myType)
                {
                    case Card.CardTypes.Pulpo: default:
                        card.gameObject.GetComponent<Image>().sprite = Pulpo;
                    break;
                    case Card.CardTypes.Elefante:
                        card.gameObject.GetComponent<Image>().sprite = Elefante;
                    break;
                    case Card.CardTypes.Tortuga:
                        card.gameObject.GetComponent<Image>().sprite = Tortuga;
                    break;
                    case Card.CardTypes.Raton:
                        card.gameObject.GetComponent<Image>().sprite = Raton;
                    break;
                    case Card.CardTypes.Tigre:
                        card.gameObject.GetComponent<Image>().sprite = Tigre;
                    break;
                    case Card.CardTypes.Conejo:
                        card.gameObject.GetComponent<Image>().sprite = Conejo;
                    break;
                    case Card.CardTypes.Abeja:
                        card.gameObject.GetComponent<Image>().sprite = Abeja;
                    break;
                    case Card.CardTypes.Oso:
                        card.gameObject.GetComponent<Image>().sprite = Oso;
                    break;
                    case Card.CardTypes.Serpiente:
                        card.gameObject.GetComponent<Image>().sprite = Serpiente;
                    break;
                    case Card.CardTypes.Cangrejo:
                        card.gameObject.GetComponent<Image>().sprite = Cangrejo;
                    break;
                    case Card.CardTypes.Tiburon:
                        card.gameObject.GetComponent<Image>().sprite = Tiburon;
                    break;
                }
            }
        }
    }

    public void SetEnemyHPUI()
    {
        foreach(Transform t in enemyZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card != null)
            {
                Text[] texts = card.gameObject.GetComponentsInChildren<Text>();
                texts[1].text = card.ThisCard.CurrentHP.ToString();
            }
        }
    }

    public void SetEnemyAttackUI()
    {
        foreach(Transform t in enemyZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card != null)
            {
                Text[] texts = card.gameObject.GetComponentsInChildren<Text>();
                texts[0].text = card.ThisCard.CardStats.Attack.ToString();
            }
        }
    }

    public void SetPlayerHPUI()
    {
        foreach(Transform t in playerZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card != null)
            {
                Text[] texts = card.gameObject.GetComponentsInChildren<Text>();
                texts[1].text = card.ThisCard.CurrentHP.ToString();
            }
        }
    }

    public void SetPlayerAttackUI()
    {
        foreach(Transform t in playerZones)
        {
            CardDragger card = t.gameObject.GetComponentInChildren<CardDragger>();
            if(card != null)
            {
                Text[] texts = card.gameObject.GetComponentsInChildren<Text>();
                texts[0].text = card.ThisCard.CardStats.Attack.ToString();
            }
        }
    }

    public void SetAttackHPUI()
    {
        SetPlayerHPUI();
        SetPlayerAttackUI();
        SetEnemyHPUI();
        SetEnemyAttackUI();
    }
}
