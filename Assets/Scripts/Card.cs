using System.Collections;
using System.Collections.Generic;

public class Card
{
    public enum CardTypes { Elefante, Tortuga, Raton, Tigre, Conejo, Abeja, Oso, Serpiente, Cocodrilo, Cangrejo, Tiburon, Pulpo }

    private CardStats myStats;
    private int currentHp;
    private int myID;

    public CardTypes myType;

    public Card(CardTypes type, int _id)
    {
        switch(type)
        {
            case CardTypes.Pulpo: default:
                myStats = new CardStats("Pulpo", 8, 8, 0);
            break;
            case CardTypes.Elefante:
                myStats = new CardStats("Elefante", 20, 20, 0);
            break;
            case CardTypes.Tortuga:
                myStats = new CardStats("Tortuga", 1, 5, 2);
            break;
            case CardTypes.Raton:
                myStats = new CardStats("Raton", 2, 1, 1);
            break;
            case CardTypes.Tigre:
                myStats = new CardStats("Tigre", 7, 7, 5);
            break;
            case CardTypes.Conejo:
                myStats = new CardStats("Conejo", 1, 1, 0);
            break;
            case CardTypes.Abeja:
                myStats = new CardStats("Abeja", 5, 1, 2);
            break;
            case CardTypes.Oso:
                myStats = new CardStats("Oso", 6, 6, 4);
            break;
            case CardTypes.Serpiente:
                myStats = new CardStats("Serpiente", 3, 1, 2);
            break;
            case CardTypes.Cangrejo:
                myStats = new CardStats("Cangrejo", 4, 4, 0);
            break;
            case CardTypes.Tiburon:
                myStats = new CardStats("Tiburon", 10, 3, 0);
            break;
        }
        myType = type;
        currentHp = myStats.HP;
        myID = _id;
    }

    #region CardProperties

    public CardStats CardStats{ get{ return myStats; } }
    public int CurrentHP{ get{ return currentHp; } set{ currentHp = value; }}
    public int MyID{ get{ return myID;} set{ myID = value; } }

    #endregion

    #region Actions

    public void ReduceHP(int pointsLost){

        if(currentHp == 0 || currentHp <= pointsLost)
            currentHp = 0;
        else
            currentHp -= pointsLost;

    }

    #endregion
}
