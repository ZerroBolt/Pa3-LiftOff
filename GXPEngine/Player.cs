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


    float turnSpeedShip = 3;
    float moveSpeedShip = 5;
    public Player(string fileName, int cols, int rows, TiledObject tiledobject = null) : base(fileName, cols, rows)
    {

        
        

    }

    
    void MoveSpaceShip()
    {
        float dx = 0;
        float dy = 0;
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
            Move(0,dx -= moveSpeedShip);
        }
        if (Input.GetKey(Key.S))
        {
            Move(0,dx += moveSpeedShip);
        }

        int delaTimeClamped = Mathf.Min(Time.deltaTime, 40);

        float vx = dx * delaTimeClamped / 1000;
        float vy = dy * delaTimeClamped / 1000;

        MoveUntilCollision(vx, 0);
        MoveUntilCollision(0, vy);


        GameObject[] collidingObjects = GetCollisions();
        foreach (GameObject collidingObject in collidingObjects)
        {
            if (collidingObject is Enemy)
            {
                collidingObject.LateDestroy();
                ((MyGame)game).DecreaseHealth();
            }

            if (collidingObject is House) 
            {
                ((MyGame)game).DecreaseHealth();

            }
        }

        }

    

   

    void Update()
    {
        
        MoveSpaceShip();

    }
}