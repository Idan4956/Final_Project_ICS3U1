using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Transactions;
using BaseMono;

public class EnemyGeneration
{
    private readonly List<Combatant> playerTroops;
    private readonly List<Combatant> enemyTroops;

    public EnemyGeneration(List<Combatant> playerTroops, List<Combatant> enemyTroops)
    {
        this.playerTroops = playerTroops;
        this.enemyTroops = enemyTroops;
    }

    public void update(Random rng)
    {
        int totalEnemies = 2;
        totalEnemies += playerTroops.Count;

        Combatant newEnemy;

        for (int i = 1; i < totalEnemies; i++)
        {
            double randomizeEnemy = rng.NextDouble();

            string enemyNum = "enemyNum" + Convert.ToString(i);

            if (randomizeEnemy >= 0.0 && randomizeEnemy <= 0.3)
            {
                newEnemy = new hog(enemyNum, rng);
            }
            else if (randomizeEnemy > 0.3 && randomizeEnemy <= 0.6)
            {
                newEnemy = new bee(enemyNum, rng);
            }
            else if (randomizeEnemy > 0.6 && randomizeEnemy <= 0.85)
            {
                newEnemy = new pixie(enemyNum, rng);
            }
            else 
            {
                newEnemy = new wanderer(enemyNum, rng);
            } 

            enemyTroops.Add(newEnemy);

            newEnemy = null;
        }
    }
}