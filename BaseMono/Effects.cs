using System;
using System.Collections.Generic;

public class Effects
{ 
    private readonly TurnManager TurnManager;

    public Effects (TurnManager turnManager)
    {
        public double poisonDmg { get; set;}
        public double bleedDmg { get; set;}
        public double burnDmg { get; set;}
        public double regenHeal { get; set;}
        public double hiddenEffect { get; set;}
        public double trappedEffect { get; set;}
        
        public double poisonDamage()
        {
            //fix this

            if (this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth * 0.10 > 2)
                poisonDmg = this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth *0.10;
            else if (this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth > 0)
                    poisonDmg = 1;
            else 
                poisonDmg = 0;

            return  poisonDmg;
        }

        public double bleedDamage()
        {
            if (this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth * 0.07 > 3)
                bleedDmg = this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth * 0.07 ;
            else if (this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth >0)
                    bleedDmg = 1;
            else 
                bleedDmg = 0;

            return bleedDmg;
        }
        public double burnDamage()
        {
            if (this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth > 2 * this.TurnManager.allCombatants[this.TurnManager.currentCharacter].burnStacks)
                burnDmg = 2 * this.TurnManager.allCombatants[this.TurnManager.currentCharacter].burnStacks;
            else if (this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth > 0)
                    burnDmg = this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth = 0;
            else 
                burnDmg = 0;

            return  burnDmg;
        }
        public double regenerationHeal()
        {         
            regenHeal = this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth * 0.10 ;

            return  regenHeal;
        }
        public double HiddenEffect()
        {
            
            //Work from here on out

            hiddenEffect = this.TurnManager.allCombatants[this.TurnManager.currentCharacter].;


            return hiddenEffect;
        }
        public double TrappedEffect()
        {
            if (this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth * 0.07 > 3)
                bleedDmg = this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth * 0.07 ;
            else if (this.TurnManager.allCombatants[this.TurnManager.currentCharacter].currentHealth >0)
                    bleedDmg = 1;
            else 
                poisonDmg = 0;

            return  poisonDmg;
        }
    }
}