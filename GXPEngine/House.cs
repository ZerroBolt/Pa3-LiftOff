using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;


public class House : AnimationSprite
{
    bool initialized = false;
    public House(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows)
    {
        Initialize(obj);
    }

    public House(TiledObject obj = null) : base("house.png", 1, 1)
    {
        Initialize(obj);
    }

    private void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
    }

    void Update()
    {
        if (!initialized)
        {
            SetXY(game.width / 2, game.height / 2);
            initialized = true;
        }
    }




}

