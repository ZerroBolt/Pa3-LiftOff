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

public class Truck : AnimationSprite
{
    public Truck(string filename, int cols, int rows, TiledObject obj = null) : base("truck.png", 6, 6)
    {
        //Sprite for player
        Console.WriteLine("space test");
    }
}