using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public abstract class EnemyCharacter : Combatant
{

    //CoolDown
    public int ability1CoolDown { get; set;}

    //attack selection
    public int attackSelect { get; set;}

    public abstract int attackSelector(Random rng);

    //apply attack selected
    public abstract void strike(List<Character> targets, TurnManager turnManager, Random rng);

    //Basic Attacks
    public abstract void basicStrike(Character target, Random rng, TurnManager turnManager);

    //Abilities
    public abstract void useSkillOne(List<Character> targets, Random rng, TurnManager turnManager);

}