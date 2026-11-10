using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using BaseMono;

public abstract class Combatant
{
    //Basic info
    public string name { get; set; }
    public double maxHealth { get; set; }
    public double currentHealth { get; set; }
    public int maxMana { get; set; }
    public int currentMana { get; set; }
    public string classType { get; set;}

    //Stats 
    public int strength { get; set; }
    public int dexterity { get; set; }
    public int constitution { get; set; }
    public int intelligence { get; set; }
    public int faith { get; set;}
    
    //Status Effects
    public int trappedStacks { get; set; }
    public int burnStacks { get; set; }
    public int tauntStacks { get; set; }
    public int regenStacks { get; set; }
    public int bleedStacks { get; set; }
    public int poisonStacks { get; set; }
    public int hiddenStacks { get; set; }
    public int guardStacks { get; set; }

    //Chances
    public double dodgeChance { get; set;}
    public double critChance { get; set;}
    public double missChance { get; set;}

    //Make Attacks check true hit and do miss and dodge stuff
    public bool trueHit;
    protected bool hitPass (TurnManager turnManager, Game1 target, Random rng)
    {
        if (trueHit)
            return true;

        //only returns if both parameters pass
        return rng.NextDouble() >= missChance && rng.NextDouble() >= dodgeChance;
    }
}