using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EnemyController : GameObject
{
    List<Zombie> enemies;
    Sprite target = null;

    int enemySpawnIntervalMs = 2000;
    int lastEnemySpawn = 0;

    public EnemyController()
    {
        enemies = new List<Zombie>();
    }

    public void SpawnEnemy()
    {
        int random = Utils.Random(0, 100);
        if (random < 85)
        {
            Zombie enemy = new Zombie();
            enemy.SetTarget(target);
            //TODO: Do we need to have the enemies in a list?
            //enemies.Add(enemy);
            AddChild(enemy);
        }
        else
        {
            Console.WriteLine("Spawning BigBoi!!");
            BigBoi enemy = new BigBoi();
            enemy.SetTarget(target);
            AddChild(enemy);
        }
    }

    void Update()
    {
        if (Time.time > lastEnemySpawn)
        {
            lastEnemySpawn = Time.time + enemySpawnIntervalMs;
            SpawnEnemy();
            if (enemySpawnIntervalMs > 200)
            {
                //Decrease spawn interval everytime an enemy spawns
                //enemySpawnIntervalMs = ((int)(enemySpawnIntervalMs * 0.99f));
                enemySpawnIntervalMs -= 50;
                Console.WriteLine(enemies.Count);
            }
        }
    }

    public void SetTarget(Sprite target)
    {
        this.target = target;
    }
}