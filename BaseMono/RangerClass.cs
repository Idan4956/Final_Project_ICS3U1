using System;
using System.Collections.Generic;

public class ranger : Character
{
    public ranger (string rangerName)
    {
        //Initial info
        name = rangerName;
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

        //Cooldown
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
                currentMana--;

                double staticDamage = 5 * (1 + (dexterity * 0.05));
                double rolledDamage = (staticDamage *(1 + (rng.Next(7,14) / 10.0)));
                
                

                int bleedRandomizer = rng.Next(1,21);

                if (bleedRandomizer == 20)
                    target.burnStacks++;

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
        }

        public override void useSkillTwo(List<Character> targets, int targetIndex, Random rng, TurnManager turnManager)
        {
            //Check
            if(targets == null || targetIndex < 0 || targetIndex >= targets.Count)
            {
                return;
            }

            //Start attack logic
            Character target = targets[targetIndex];

            if (currentMana >= 2 && ability2CoolDown <= 0)
            {
                currentMana -= 2;

                double staticDamage = 6 * (1 + (dexterity * 0.04));
                double rolledDamage = (staticDamage * (1 + (rng.Next(7, 14) / 10.0)));

                if (target.currentHealth > rolledDamage)
                {
                    target.currentHealth -= rolledDamage;
                }
                else 
                {
                    target.currentHealth = 0;
                }

                double staticDamage2 = 6 * (1 + (dexterity * 0.04));
                double rolledDamage2 = (staticDamage2 * (1 + (rng.Next(7, 14) / 10.0)));

                if (target.currentHealth > rolledDamage2)
                {
                    target.currentHealth -= rolledDamage2;
                }
                else 
                {
                    target.currentHealth = 0;
                }

                ability2CoolDown = 2;

                turnManager.isTurnDone = true;
                turnManager.advanceTurn();


            }

            ability2CoolDown--;

        }

}