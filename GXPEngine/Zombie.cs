using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Zombie : Enemy
{
    const string imageLocation = "Zom.png";

    //TODO: change cols and rows for animation sprite
    public Zombie() : base(imageLocation, 1, 1)
    {
        //TODO: setcycle for animation sprite
        //SetCycle(0, 4);
        health = 1;
        damage = 1;
        speed = 1f;
        scale = 0.15f;

        base.Initialize();
    }

    void Update()
    {
        base.Update();
    }
}