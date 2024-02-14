using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;


public class Level : GameObject
{


    public Level(string mapName)
    {
        TiledLoader loader = new TiledLoader(mapName);
        loader.rootObject = this;
        loader.addColliders = false;
        loader.LoadTileLayers(0); //background
        loader.addColliders = true;
        loader.LoadTileLayers(1); //interaction
        loader.autoInstance = true;
        loader.LoadObjectGroups(0); /// Objects
        loader.LoadObjectGroups(1);


        Console.WriteLine("level loaded");

    }



    public void Update()
    {



    }
}