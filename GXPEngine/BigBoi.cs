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
    const string imageLocation = "Zom.png";

    //TODO: change cols and rows for animation sprite
    public BigBoi() : base(imageLocation, 1, 1)
    {
        //TODO: setcycle for animation sprite
        //SetCycle(0, 4);
        health = 2;
        damage = 2;
        speed = 0.5f;
        scale = 0.25f;

        base.Initialize();
    }

    void Update()
    {
        base.Update();
    }
}
