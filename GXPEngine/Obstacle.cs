using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Obstacle : AnimationSprite
{

    public Obstacle(string filename, int cols, int rows, TiledObject tiledobject = null) : base(filename, cols, rows)
    { 

    }

    private void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
    }
}
