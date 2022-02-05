using System.Collections;
using System.Collections.Generic;

public class CardStats
{
    private int hp, attack, defense, manaCost;
    private string name;

    public CardStats(string name, int hp, int attack, int defense, int manaCost){
            this.name = name;
            this.hp = hp;
            this.attack = attack;
            this.defense = defense;
            this.manaCost = manaCost;
    }

    public string Name { get {return name;} }
    public int HP { get {return hp;} }
    public int Attack { get {return attack;} }
    public int Defense { get {return defense;} }
    public int ManaCost { get {return manaCost;} }
}
