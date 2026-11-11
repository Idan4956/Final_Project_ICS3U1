using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Transactions;
using BaseMono;
using RandomNameGeneratorLibrary;

public class EnemyGeneration
{
    private readonly List<Combatant> playerTroops;
    private readonly List<Combatant> enemyTroops;
    private RandomNameGeneratorLibrary.StarNameGenerator enemyNameGenerator;

    public EnemyGeneration(List<Combatant> playerTroops, List<Combatant> enemyTroops)
    {
        this.playerTroops = playerTroops;
        this.enemyTroops = enemyTroops;

        this.enemyNameGenerator = new StarNameGenerator();
    }

    public void update(Random rng)
    {
        int totalEnemies = 2;
        totalEnemies += playerTroops.Count;

        Combatant newEnemy;

        for (int i = 1; i < totalEnemies; i++)
        {
            double randomizeEnemy = rng.NextDouble();

            string enemyName = enemyNameGenerator.GenerateRandomStarName();

            if (randomizeEnemy >= 0.0 && randomizeEnemy <= 0.3)
            {
                newEnemy = new hog(enemyName, rng);
            }
            else if (randomizeEnemy > 0.3 && randomizeEnemy <= 0.6)
            {
                newEnemy = new bee(enemyName, rng);
            }
            else if (randomizeEnemy > 0.6 && randomizeEnemy <= 0.85)
            {
                newEnemy = new pixie(enemyName, rng);
            }
            else 
            {
                newEnemy = new wanderer(enemyName, rng);
            } 

            enemyTroops.Add(newEnemy);

            newEnemy = null;
        }
    }
}