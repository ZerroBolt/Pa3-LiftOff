using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class BigBoi : Enemy
{
    const string imageLocation = "big_guy.png";
    float animSpeed = 0.20f;

    public BigBoi() : base(imageLocation, 4, 3)
    {
        health = 2;
        damage = 2;
        speed = 0.5f;
        scale = 1f;

        SetCycle(0, 12);

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
