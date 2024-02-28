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
    bool Moving = false;

    private int _score = 0;
    public int score
    {
        get { return _score; }
        set { _score = value; }
    }

    private string _playerName = null;
    public string playerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }

    public Player(string fileName, int cols, int rows, TiledObject tiledobject = null) : base(fileName, cols, rows)
    {

    }

    void RotateTruck()
    {
        if (MyGame.isPortOpen)
        {
            //TODO: Rotate the truck based on the steering wheel rotation
            // Rotation 0 - 30 && speed 91 - 100 = drift left

            // Rotation 250 - 280 && speed 91 - 100 = drift right

            // Rotation 0 - 120 = rotate left

            // Rotation 160 - 280 = rotation right

            // Rotation 121 - 159 = move forward

        }
        else
        {
            if (Input.GetKey(Key.A))
            {
                rotation -= turnSpeedTruck;
                Moving = true;
            }

            if (Input.GetKey(Key.D))
            {
                rotation += turnSpeedTruck;
                Moving = true;
            }
        }
    }

    float MoveTruck(float dx)
    {
        //TODO: Move the truck based on the shift position
        // Shift 0 - 39 = backwards

        // Shift 40 - 90 = forwards

        // Shift 91 - 100 = drift


        return dx;
    }

    void MoveTruck()
    {
        Moving = false;
        float dx = 0;
        float dy = 0;

        RotateTruck();

        if (MyGame.isPortOpen)
        {
            dx = MoveTruck(dx);
        }
        else
        {
            if (Input.GetKey(Key.W))
            {
                Move(0, dx -= moveSpeedTruck);
                Moving = true;
            }
            if (Input.GetKey(Key.W) && Input.GetKey(Key.ENTER))
            {
                Move(0, dx -= moveSpeedTruck + 0.5f);
                Moving = true;
            }

            if (Input.GetKey(Key.S))
            {
                Move(0, dx += moveSpeedTruck / 2);
                Moving = true;
            }
        }


        int delaTimeClamped = Mathf.Min(Time.deltaTime, 40);

        float vx = dx * delaTimeClamped / 1000;
        float vy = dy * delaTimeClamped / 1000;

        MoveUntilCollision(vx, 0);
        MoveUntilCollision(0, vy);

        GameObject[] collidingObjects = GetCollisions();
        foreach (GameObject collidingObject in collidingObjects)
        {
            if (collidingObject is Enemy && Moving)
            {
                ((MyGame)game).IncreaseScore();
                ((MyGame)game).StartCombo();
                ((MyGame)game).ResetComboTime();
                ((MyGame)game).IncreaseKills();

                ((MyGame)game).SpawnScore();
                ((MyGame)game).scorehud.UpdateScoreOnCar(collidingObject as Enemy);

                collidingObject.LateDestroy();

                switch (Utils.Random(0, 3))
                {
                    case 0: dyingzombie1.Play(false, 0, 1); break;
                    case 1: dyingzombie2.Play(false, 0, 1); break;
                    case 2: dyingzombie3.Play(false, 0, 1); break;
                }
            }

            if (collidingObject is House)
            {
                //SetXY(400, 400);
            }
            if (collidingObject is Obstacle)
            {
                hitobstacle.Play(false, 0, 0.5f);
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

    Sound movingcar = new Sound("car_moving.wav", false);
    Sound idlecar = new Sound("car_idle.wav", false);
    Sound hitobstacle = new Sound("obstacle_hit.wav", false);
    Sound dyingzombie1 = new Sound("zombie_dying1.wav", false);
    Sound dyingzombie2 = new Sound("zombie_dying2.wav", false);
    Sound dyingzombie3 = new Sound("zombie_dying3.wav", false);

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

        if (Moving)
        {


            movingcar.Play(false, 0, 0);

            Console.WriteLine("moving");
        }
        else
        {
            idlecar.Play(false, 0, 0);

            Console.WriteLine("not moving");
        }
        Console.WriteLine(Time.time);
    }
}