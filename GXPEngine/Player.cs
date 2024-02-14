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

public class Player : AnimationSprite
{


    float turnSpeedShip = 0.6f;
    float moveSpeedShip = 1;
    public Player(string fileName, int cols, int rows, TiledObject tiledobject = null) : base(fileName, cols, rows)
    {

        
        Console.WriteLine("test");

    }

 
    void MoveSpaceShip()
    {

        if (Input.GetKey(Key.A))
        {
            rotation -= turnSpeedShip;
        }
        if (Input.GetKey(Key.D))
        {
            rotation += turnSpeedShip;
        }

        if (Input.GetKey(Key.W))
        {
            Move(0, -moveSpeedShip);
        }
        if (Input.GetKey(Key.S))
        {
            Move(0, moveSpeedShip);
        }
    }


    void Update()
    {

        MoveSpaceShip();

    }
}