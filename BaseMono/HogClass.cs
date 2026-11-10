using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

public class hog : EnemyCharacter
{
    public hog (string hogName, Random rng)
    {
        //Initial info
        name = hogName;
        maxHealth = rng.Next(15, 26);
        currentHealth = maxHealth;
        maxMana = 4;
        currentMana = 0;
        classType = "hog";

        //Initial Stats
        strength = rng.Next(0, 10);
        dexterity = rng.Next(0, 5);
        constitution = 0;
        intelligence = rng.Next(0, 5);
        faith = rng.Next(0, 5);

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
        if (currentMana >= 2 && ability1CoolDown == 0)
        {
            attackSelect = rng.Next(1,3);
        }

        else 
            attackSelect = 1;

        return attackSelect;
    }

    public override int randomizeAttack(Random rng, TurnManager turnManager)
    {
        List<int> playerTroops = new List<int>();
        int randomPlayer = 1;
        
        for (int i = 0; i < turnManager.allCombatants.Count; i++)
        {
            if (turnManager.allCombatants[i] is Character)
            {
                playerTroops.Add(i);
            }
        }
        
        randomPlayer = playerTroops[rng.Next(0, playerTroops.Count)];

        return randomPlayer;
    }

    public override void strike(List<Combatant> targets, TurnManager turnManager, Random rng)
    {
        int attackPattern = this.attackSelector(rng);  

        if (attackPattern == 1)
            this.basicStrike(targets[this.randomizeAttack(rng, turnManager)], rng, turnManager); 
        else if (attackPattern == 2)
            this.useSkillOne(targets, rng, turnManager);

    }

    public override void basicStrike(Combatant target, Random rng, TurnManager turnManager)
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

    public override void useSkillOne(List<Combatant> targets, Random rng, TurnManager turnManager)
    {
        if (targets == null || targets.Count == 0)
        {
            return;
        }

        for(int targetIndex = 0; targetIndex < targets.Count; targetIndex++)
        {
            Combatant target = targets[targetIndex];

            double staticDamage = 6 * (1 + (this.strength * 0.05));
            double rolledDamage = staticDamage * (rng.Next(7,14) /10.0);

            if (target.currentHealth > rolledDamage)
                target.currentHealth -= rolledDamage;              

            else
                target.currentHealth = 0;

        }

        currentMana-= 2;
        ability1CoolDown = 3;

        turnManager.isTurnDone = true;
        turnManager.advanceTurn();
    }

}