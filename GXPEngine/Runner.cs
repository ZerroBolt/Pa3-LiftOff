using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Runner : Enemy
{
    const string imageLocation = "runner_zombie.png";
    float animSpeed = 0.45f;

    public Runner() : base(imageLocation, 5, 4)
    {
        health = 1;
        damage = 1;
        speed = 1f;
        scale = 0.9f;

        SetCycle(0, 18);

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
