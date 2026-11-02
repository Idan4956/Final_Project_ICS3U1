using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

public class wanderer : EnemyCharacter
{
    public wanderer (string wandererName, Random rng)
    {
        //Initial info
        name = wandererName;
        maxHealth = rng.Next(20, 36);
        currentHealth = maxHealth;
        maxMana = 4;
        currentMana = 0;

        //Initial Stats
        strength = rng.Next(0, 10);
        dexterity = rng.Next(0, 10);
        constitution = 0;
        intelligence = rng.Next(0, 10);
        faith = rng.Next(0, 10);

        //StatusStacks
        trappedStacks = 0;
        burnStacks = 0;
        tauntStacks = 0;
        regenStacks = 0;
        bleedStacks = 0;
        poisonStacks = 0;
        hiddenStacks = 0;
        guardStacks = 0;

        //cooldown
        ability1CoolDown = 0;
        
        //attack selector
        attackSelect = 1;

        //Chances
        dodgeChance = 10;
        critChance = 10;
        missChance = 10;
    }
    public override int attackSelector(Random rng)
    {
        if (currentMana >= 3 && ability1CoolDown == 0)
        {
            attackSelect = rng.Next(1,3);
        }

        else 
            attackSelect = 1;

        return attackSelect;
    }

    public override void strike(List<Character> targets, TurnManager turnManager, Random rng)
    {
        int attackPattern = this.attackSelector(rng);  

        if (attackPattern == 1)
            this.basicStrike(targets[0], rng, turnManager); 
        else if (attackPattern == 2)
            this.useSkillOne(targets, rng, turnManager);

    }

    public override void basicStrike(Character target, Random rng, TurnManager turnManager)
    {
        double staticDamage = 2 * (1 + (this.strength * 0.05));

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

    public override void useSkillOne(List<Character> targets, Random rng, TurnManager turnManager)
    {
        if (targets == null || targets.Count == 0)
        {
            return;
        }

        //Make sure this attacks all player Units

        for(int targetIndex = 0; targetIndex < targets.Count; targetIndex++)
        {
            Combatant target = targets[targetIndex];

            double staticDamage = (5 + 2 * targetIndex) * (1 + (this.strength * 0.05));
            double rolledDamage = staticDamage * (rng.Next(7,14) /10.0);

            if (target.currentHealth > rolledDamage)
                target.currentHealth -= rolledDamage;              

            else
                target.currentHealth = 0;

        }

        currentMana-= 3;
        ability1CoolDown = 4;

        turnManager.isTurnDone = true;
        turnManager.advanceTurn();
    }

}