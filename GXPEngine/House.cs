using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


public class House : AnimationSprite
{
    public House(string filename, int cols, int rows,TiledObject tiledobject = null) : base(filename, cols, rows)
    {
        
    }

    private void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
    }




}

