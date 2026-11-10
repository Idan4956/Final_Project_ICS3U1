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

    public void cleanList()
    {
        for (int i = 0; i < allCombatants.Count; i++)
        {
            if (allCombatants[i].currentHealth <= 0)
            {
                allCombatants.Remove(allCombatants[i]);
            }
        }
    }

    public void advanceTurn ()
    {   
        cleanList();

        if (!isTurnDone || allCombatants.Count == 0)
            return;

        isTurnDone = false;
        
        currentCharacter = (currentCharacter + 1)% allCombatants.Count;
    }

    public bool isCurrentCharacterPlayer()
    {
        return allCombatants[currentCharacter] is fighter || allCombatants[currentCharacter] is ranger
        || allCombatants[currentCharacter] is rogue || allCombatants[currentCharacter] is wizard
        || allCombatants[currentCharacter] is cleric; 
    }
}