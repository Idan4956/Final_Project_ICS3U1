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

    //Randomize player to attack
    public abstract int randomizeAttack(Random rng, TurnManager turnManager);

    //apply attack selected
    public abstract void strike(List<Combatant> targets, TurnManager turnManager, Random rng);

    //Basic Attacks
    public abstract void basicStrike(Combatant target, Random rng, TurnManager turnManager);

    //Abilities
    public abstract void useSkillOne(List<Combatant> targets, Random rng, TurnManager turnManager);

}