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

        //Chances
        dodgeChance = 10;
        critChance = 10;
        missChance = 10;
    }

    //Abilities
        public override void useSkillOne(Combatant target, Random rng, TurnManager turnManager)
        {
            //Start heal logic
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

        public override void useSkillTwo(IReadOnlyList<Combatant> targets, Random rng, TurnManager turnManager)
        {
            if (targets == null || targets.Count == 0)
            {
                return;
            }

            //Start heal logic
            if (currentMana >= 2 && ability2CoolDown <= 0)
            {
                currentMana -= 2;
                for (int targetIndex = 0; targetIndex < targets.Count; targetIndex++)
                {
                    Combatant target = targets[targetIndex];

                    double staticHeal = 6 * (1 + (dexterity * 0.04));
                    double rolledHeal = (staticHeal * (1 + (rng.Next(7, 14) / 10.0)));

                    if (target.currentHealth > 0.0)
                        target.currentHealth += rolledHeal;
                }

                ability2CoolDown = 3;

                turnManager.isTurnDone = true;
                turnManager.advanceTurn();
            }

            ability2CoolDown--;

        }

}