﻿using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ObstacleController : GameObject
{
    List<Obstacle> obstacles;
    
    int obstacleSpawnIntervalMs = 20000;
    int lastObstacleSpawn = 0;

    public ObstacleController()
    {
        obstacles = new List<Obstacle>();
    }

    public void SpawnObstacle()
    {
        Obstacle obstacle = new Obstacle();
       
        //TODO: Do we need to have the enemies in a list?
        //enemies.Add(enemy);
        AddChild(obstacle);
    }

    void Update()
    {
        if (Time.time > lastObstacleSpawn)
        {
            lastObstacleSpawn = Time.time + obstacleSpawnIntervalMs;
            SpawnObstacle();
        }
    }


}