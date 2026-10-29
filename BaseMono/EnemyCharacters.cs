using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public abstract class EnemyCharacter
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

    //Status effects
    public int trappedStacks { get; set;}
    public int burnStacks { get; set;}
    public int tauntStacks { get; set;}
    public int regenStacks { get; set;}
    public int bleedStacks { get; set;}
    public int poisonStacks { get; set;}
    public int hiddenStacks { get; set;}
    public int guardStacks { get; set;}

    //CoolDown
    public int ability1CoolDown { get; set;}

    //attack selection
    public int attackSelect { get; set;}

    public abstract int attackSelector(Random rng);

    //apply attack selected
    public abstract void strike(List<Character> targets, TurnManager turnManager, Random rng);

    //Basic Attacks
    public abstract void basicStrike(Character target, Random rng, TurnManager turnManager);

    //Abilities
    public abstract void useSkillOne(List<Character> targets, Random rng, TurnManager turnManager);

}