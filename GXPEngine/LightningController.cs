using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class LightningController : GameObject
{
    List<Lightning> lightnings;



    int lightningSpawnIntervalMs = 8000;
    int lastLightningSpawn = 2000;



    
   

    public LightningController()
    {
        lightnings = new List<Lightning>();
    }
    
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
            

            
        }
       
    }
}