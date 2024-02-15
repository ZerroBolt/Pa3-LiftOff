using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Powerup : AnimationSprite
{

    public Powerup(string filename, int cols, int rows, TiledObject obj = null) : base("playerShip1_blue.png", 6, 6)
    {

        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
    }



}