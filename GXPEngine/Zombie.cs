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
    const string imageLocation = "basic_zombie.png";
    float animSpeed = 0.15f;

    //TODO: change cols and rows for animation sprite
    public Zombie() : base(imageLocation, 3, 3)
    {
        //TODO: setcycle for animation sprite
        //SetCycle(0, 4);
        health = 1;
        damage = 1;
        speed = 1f;
        scale = 1f;

        SetCycle(0, 7);

        base.Initialize();
    }

    void Update()
    {
        float oldx = x;
        base.Update();

        Animate(animSpeed);

        if (oldx > x)
        {
            Mirror(true, false);
        }
        else
        {
            Mirror(false, false);
        }
    }
}