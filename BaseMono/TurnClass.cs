using System;
using System.Collections.Generic;

public class TurnManager
{
    public List<Character> allCombatants = new List<Character>();
    public int currentCharacter = 0;

    public bool isTurnDone = false;

    public void startTurn(Effects Effects)
    {
        if (allCombatants[currentCharacter].currentMana < allCombatants[currentCharacter].maxMana)
            allCombatants[currentCharacter].currentMana++;

        if (allCombatants[currentCharacter].poisonStacks >= 1)
        {
            if (allCombatants[currentCharacter].currentHealth - Effects.poisonDmg > 0)
            allCombatants[currentCharacter].currentHealth -= Effects.poisonDmg;
            allCombatants[currentCharacter].poisonStacks--;
        }
    }

    public void advanceTurn ()
    {
        if (isTurnDone == true && currentCharacter <= allCombatants.Count)
        {
            isTurnDone = false;

            currentCharacter++;
        }
        else if (isTurnDone == true && currentCharacter > allCombatants.Count)
        {
            isTurnDone = false;

            currentCharacter = 0;
        }
    }
}