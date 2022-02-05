using System.Collections;
using System.Collections.Generic;

public class Card
{
    public enum CardTypes { Pulpo, Michi }

    private CardStats myStats;
    private int currentHp, currentAttack, currentDefense;

    public Card(CardTypes type)
    {
        switch(type)
        {
            case CardTypes.Pulpo: default:
                myStats = new CardStats("Pulpo", 10, 5, 10, 1);
            break;
            case CardTypes.Michi:
                myStats = new CardStats("Michi", 7, 10, 5, 1);
            break;
        }        
        SetStartingStats();
    }

    private void SetStartingStats(){
        currentHp = myStats.HP;
        currentAttack = myStats.Attack;
        currentAttack = myStats.Defense;
    }

    #region CardProperties

    public CardStats CardStats{ get{ return myStats; } }
    public int CurrentHP{ get{ return currentHp; } }
    public int CurrentAttack{ get{ return currentAttack; } }
    public int CurrentDefense{ get{ return currentDefense; } }

    #endregion

    #region Actions

    public void ReduceHP(int pointsLost){

        if(currentHp == 0 || currentHp <= pointsLost)
            currentHp = 0;
        else
            currentHp -= pointsLost;

    }

    public void ReduceAttack(int pointsLost){

        if(currentAttack == 1 || currentAttack <= pointsLost)
            currentAttack = 1;
        else
            currentAttack -= pointsLost;
            
    }

    public void ReduceDefense(int pointsLost){

        if(currentDefense == 1 || currentDefense <= pointsLost)
            currentDefense = 1;
        else
            currentDefense -= pointsLost;
            
    }

    #endregion
}
