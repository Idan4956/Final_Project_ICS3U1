using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public abstract class Character : Combatant
{
    //CoolDown
    public int ability2CoolDown { get; set;}

    //Basic Attacks
    public virtual void basicStrike(Combatant target, Random rng, TurnManager turnManager)
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
    public abstract void useSkillOne(Combatant target, Random rng, TurnManager turnManager);
    public abstract void useSkillTwo(IReadOnlyList<Combatant> targets, Random rng, TurnManager turnManager);

}