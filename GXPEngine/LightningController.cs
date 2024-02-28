using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class LightningController : GameObject
{
    List<Lightning> lightnings;



    int lightningSpawnIntervalMs = 2000;
    int lastLightningSpawn = 2000;

   
    


    public LightningController()
    {
        lightnings = new List<Lightning>();
    }
    Lightning lightning = new Lightning();
    public void SpawnLightning()
    {
        Lightning lightning = new Lightning();


        //TODO: Do we need to have the enemies in a list?
        //enemies.Add(enemy);
        AddChild(lightning);
        Console.WriteLine("add lightning");
      

    }
 
    void Update()
    {
        if (Time.time > lastLightningSpawn)
        {
            lastLightningSpawn = Time.time + lightningSpawnIntervalMs;
            SpawnLightning();
            

            if (lightningSpawnIntervalMs > 200)
            {
                //Decrease spawn interval everytime an enemy spawns
                lightningSpawnIntervalMs = ((int)(lightningSpawnIntervalMs * 0.99f));
                //enemySpawnIntervalMs -= 10;

            }
        }
       
    }


}