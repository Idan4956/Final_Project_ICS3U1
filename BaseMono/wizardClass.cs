using System;
using System.Collections.Generic;

public class wizard : Character
{
    public wizard (string wizardName)
    {
        //Initial info
        name = wizardName;
        maxHealth = 30;
        currentHealth = maxHealth;
        maxMana = 4;
        currentMana = 0;

        //Initial Stats
        strength = 0;
        dexterity = 0;
        constitution = 0;
        intelligence = 3;
        faith = 0;

        //cooldown
        ability2CoolDown = 0;

        //Chances
        dodgeChance = 10;
        critChance = 10;
        missChance = 10;
    }

    //Abilities
        public override void useSkillOne(Combatant target, Random rng, TurnManager turnManager)
        {
            
            //Start attack logic
            if (currentMana >= 1)
            {
                currentMana --;

                double staticDamage = 4 * (1 + (intelligence * 0.05));
                double rolledDamage = (staticDamage *(1 + (rng.Next(7,14) / 10.0)));

                if (target.currentHealth > rolledDamage)
                {
                    target.currentHealth -= rolledDamage;

                    target.burnStacks++;
                }

                else
                {
                    target.currentHealth = 0;
                }

                turnManager.isTurnDone = true;
                turnManager.advanceTurn();
            }
        }

        public override void useSkillTwo(IReadOnlyList<Combatant> targets, Random rng, TurnManager turnManager)
        {
            if (targets == null || targets.Count == 0)
            {
                return;
            }

            //Start attack logic
            if (currentMana >= 2 && ability2CoolDown <= 0)
            {
                currentMana -= 2;
                for (int targetIndex = 0; targetIndex < targets.Count; targetIndex++)
                {
                    Combatant target = targets[targetIndex];

                    double staticDamage = 2 * (1 + (intelligence * 0.04));
                    double rolledDamage = (staticDamage * (1 + (rng.Next(7, 14) / 10.0)));

                    if (target.currentHealth > rolledDamage)
                    {
                        target.currentHealth -= rolledDamage;
                        target.poisonStacks += 2;
                    }
                    else 
                        target.currentHealth = 0;
                }
                
                ability2CoolDown = 3;

                turnManager.isTurnDone = true;
                turnManager.advanceTurn();
            }

            ability2CoolDown--;

        }

}