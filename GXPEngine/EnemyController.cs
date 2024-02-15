using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class EnemyController : GameObject
{
    List<Enemy> enemies;
    Sprite target = null;

    int enemySpawnIntervalMs = 2000;
    int lastEnemySpawn = 0;

    public EnemyController()
    {
        enemies = new List<Enemy>();
    }

    public void SpawnEnemy()
    {
        Enemy enemy = new Enemy();
        enemy.SetTarget(target);
        //TODO: Do we need to have the enemies in a list?
        //enemies.Add(enemy);
        AddChild(enemy);
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
                enemySpawnIntervalMs = ((int)(enemySpawnIntervalMs * 0.99f));
                //enemySpawnIntervalMs -= 10;
                Console.WriteLine(enemies.Count);
            }
        }
    }

    public void SetTarget(Sprite target)
    {
        this.target = target;
    }
}
