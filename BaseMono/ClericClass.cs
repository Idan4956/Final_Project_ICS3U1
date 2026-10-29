using System;
using System.Collections.Generic;

public class cleric : Character
{
    public cleric (string clericName)
    {
        //Initial info
        name = clericName;
        maxHealth = 30;
        currentHealth = maxHealth;
        maxMana = 4;
        currentMana = 0;

        //Initial Stats
        strength = 0;
        dexterity = 0;
        constitution = 0;
        intelligence = 0;
        faith = 3;

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

            //Start heal logic
            Character target = targets[targetIndex];

            if (currentMana >= 1)
            {
                currentMana --;

                double staticHeal = 4 * (1 + (faith * 0.05));
                double rolledHeal = (staticHeal *(1 + (rng.Next(7,14) / 10.0)));

                if (target.currentHealth > 0.0)
                {
                    target.currentHealth += rolledHeal;
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

            //Start heal logic
            Character target = targets[targetIndex];

            if (currentMana >= 2 && ability2CoolDown <= 0)
            {
                currentMana -= 2;

                double staticHeal = 6 * (1 + (dexterity * 0.04));
                double rolledHeal = (staticHeal * (1 + (rng.Next(7, 14) / 10.0)));

                if (target.currentHealth > 0.0)
                {
                    target.currentHealth += rolledHeal;
                }

                ability2CoolDown = 3;

                turnManager.isTurnDone = true;
                turnManager.advanceTurn();
            }

            ability2CoolDown--;

        }

}