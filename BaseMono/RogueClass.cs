using System;
using System.Collections.Generic;

public class rogue : Character
{
    public rogue (string rogueName)
    {
        //Initial info
        name = rogueName;
        maxHealth = 30;
        currentHealth = maxHealth;
        maxMana = 4;
        currentMana = 0;

        //Initial Stats
        strength = 0;
        dexterity = 3;
        constitution = 0;
        intelligence = 0;
        faith = 0;

        //cooldown
        ability2CoolDown = 0;
    }

    //Abilities
        public override void useSkillOne(List<Character> targets, int targetIndex, Random rng, TurnManager turnManager)
        {
            //Check for safety
            if (targets == null || targetIndex < 0 || targetIndex >= targets.Count)
            {
                return;
            }

            //Start attack logic
            Character target = targets[targetIndex];

            if (currentMana >= 1)
            {
                currentMana --;

                double staticDamage = 4 * (1 + (dexterity * 0.05));
                double rolledDamage = (staticDamage *(1 + (rng.Next(7,14) / 10.0)));

                if (target.currentHealth > rolledDamage)
                {
                    target.currentHealth -= rolledDamage;

                    target.trappedStacks += 2;
                }

                else
                {
                    target.currentHealth = 0;
                }

                turnManager.isTurnDone = true;
                turnManager.advanceTurn();
            }
        }

        public override void useSkillTwo(List<Character> targets, int targetIndex, Random rng, TurnManager turnManager)
        {
            //Check
            if(targets == null || targetIndex < 0 || targetIndex >= targets.Count)
            {
                return;
            }

            //Start hidden logic
            Character target = targets[targetIndex];

            if (currentMana >= 2 && ability2CoolDown <= 0)
            {
                currentMana -= 2;

                target.hiddenStacks += 2;

                ability2CoolDown = 4;

                turnManager.isTurnDone = true;
                turnManager.advanceTurn();
            }

            ability2CoolDown--;

        }

}