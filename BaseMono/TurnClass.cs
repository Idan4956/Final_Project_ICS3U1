using System;
using System.Collections.Generic;

public class TurnManager
{
    public List<Combatant> allCombatants = new List<Combatant>();
    public int currentCharacter = 0;

    public bool isTurnDone = false;

    public void startTurn(Effects Effects)
    {
        if (allCombatants[currentCharacter].currentMana < allCombatants[currentCharacter].maxMana)
            allCombatants[currentCharacter].currentMana++;
        
        //Add for all status's
        if (allCombatants[currentCharacter].poisonStacks >= 1)
        {
            double damage = Effects.poisonDamage();

            allCombatants[currentCharacter].currentHealth -= damage;
            allCombatants[currentCharacter].poisonStacks--;
        }

        if (allCombatants[currentCharacter].bleedStacks >= 1)
        {
            double damage = Effects.bleedDamage();

            allCombatants[currentCharacter].currentHealth -= damage;
            allCombatants[currentCharacter].bleedStacks--;
        }

        if (allCombatants[currentCharacter].burnStacks >= 1)
        {
            double damage = Effects.burnDamage();

            allCombatants[currentCharacter].currentHealth -= damage;
            allCombatants[currentCharacter].burnStacks--;
        }

        if (allCombatants[currentCharacter].regenStacks >= 1)
        {
            double heal = Effects.regenerationHeal();

            allCombatants[currentCharacter].currentHealth += heal;
            allCombatants[currentCharacter].regenStacks--;
        }

        if (allCombatants[currentCharacter].trappedStacks >= 1)
        {
            bool trapped = Effects.TrappedEffect();

            allCombatants[currentCharacter].trueHit = trapped;
            allCombatants[currentCharacter].trappedStacks--;
        }

        else
        {
            allCombatants[currentCharacter].trueHit = false;
        }
    }

    public void advanceTurn ()
    {   if (!isTurnDone || allCombatants.Count == 0)
        if (isTurnDone == true && currentCharacter < allCombatants.Count)
        {
            isTurnDone = false;

            currentCharacter++;
        }
        else if (isTurnDone == true && currentCharacter >= allCombatants.Count)
        {
            isTurnDone = false;

            currentCharacter = 0;
        }
    }
}