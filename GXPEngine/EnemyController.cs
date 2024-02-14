using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class EnemyController : GameObject
{
    List<Enemy> enemies;
    Sprite target;
    public EnemyController(Sprite target)
    {
        enemies = new List<Enemy>();
        this.target = target;

        //TODO: change this to random interval
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = new Enemy();
        enemy.SetTarget(target);
        enemies.Add(enemy);
        AddChild(enemy);
    }
}
