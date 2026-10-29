using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public abstract class Character
{
    //Basic Info
    public string name { get; set;}
    public double maxHealth { get; set;}
    public double currentHealth { get; set;}
    public int maxMana { get; set;}
    public int currentMana { get; set;}

    //Stats
    public int strength { get; set;}
    public int dexterity { get; set;}
    public int constitution { get; set;}
    public int intelligence { get; set;}
    public int faith { get; set;}

    //Status effects length
    public int trappedStacks { get; set;}
    public int burnStacks { get; set;}
    public int tauntStacks { get; set;}
    public int regenStacks { get; set;}
    public int bleedStacks { get; set;}
    public int poisonStacks { get; set;}
    public int hiddenStacks { get; set;}
    public int guardStacks { get; set;}

    //CoolDown
    public int ability2CoolDown { get; set;}

    //Basic Attacks
    public virtual void basicStrike(Character target, Random rng, TurnManager turnManager)
    {
        double staticDamage = 4 * (1 + (this.strength * 0.05));

        double rolledDamage = staticDamage * (rng.Next(7,14) /10.0);

        if (target.currentHealth > rolledDamage)
        {
            target.currentHealth -= rolledDamage;              
        }
        else
        {
            target.currentHealth = 0;
        }

        turnManager.isTurnDone = true;
        turnManager.advanceTurn();
    }

    public virtual void guard(TurnManager turnManager)
    {
        guardStacks ++;

        turnManager.isTurnDone = true;
        turnManager.advanceTurn();
    }

    public virtual void focus(TurnManager turnManager)
    {
        if (currentMana < maxMana)
        {
            currentMana++;
        }
        turnManager.isTurnDone = true;
        turnManager.advanceTurn();
    }


    //Abilities
    public abstract void useSkillOne(List<Character> targets, int targetIndex, Random rng, TurnManager turnManager);
    public abstract void useSkillTwo(List<Character> targets, int targetIndex, Random rng, TurnManager turnManager);

}