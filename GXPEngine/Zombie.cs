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

    public Zombie() : base(imageLocation, 3, 3)
    {
        health = 1;
        damage = 1;
        speed = 1f;
        scale = 1f;

        SetCycle(0, 8);

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