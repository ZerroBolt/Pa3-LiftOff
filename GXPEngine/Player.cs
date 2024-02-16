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


    float  turnSpeedTruck = 3;
    float moveSpeedTruck = 5;
    int slowdurationMs = 2000;
    int slowtime = 0;
    bool Slowed = false;
    public Player(string fileName, int cols, int rows, TiledObject tiledobject = null) : base(fileName, cols, rows)
    {

        


    }


    void MoveTruck()
    {
        float dx = 0;
        float dy = 0;
        if (Input.GetKey(Key.A))
        {
            rotation -= turnSpeedTruck;
        }
        
        if (Input.GetKey(Key.D))
        {
            rotation += turnSpeedTruck;
        }

        if (Input.GetKey(Key.W))
        {
            Move(0, dx -= moveSpeedTruck);
        }
        if (Input.GetKeyUp(Key.W))
        {
            Move(0, dx -= moveSpeedTruck * 0.98f);
        }
        if (Input.GetKey(Key.S))
        {
            Move(0, dx += moveSpeedTruck);
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

                ((MyGame)game).IncreaseScore();

            }

            if (collidingObject is House)
            {
                SetXY(400, 400);

            }
            if (collidingObject is Obstacle)
            {

                collidingObject.LateDestroy();
                Slowplayer();
               
                
                
               
            }

        }

    }
    void Slowplayer()
    {
        moveSpeedTruck = moveSpeedTruck * 0.2f;
        turnSpeedTruck = turnSpeedTruck * 0.2f;
        Slowed = true;
        
        
        Console.WriteLine("Slowed");
        slowtime = Time.time + slowdurationMs;
        
    }




    void Update()
    {

        MoveTruck();
        if (Time.time > slowtime && Slowed)
        {
            
            Slowed = false;
            
        }
        if (Slowed == false)
        {
            moveSpeedTruck = 5;
            turnSpeedTruck = 3;
        }

        
    }
}