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
        if (Input.GetKey(Key.W) && Input.GetKey(Key.ENTER))
        {
            Move(0, dx -= moveSpeedTruck + 0.5f);
        }

        if (Input.GetKey(Key.S))
        {
            Move(0, dx += moveSpeedTruck / 2);
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
                ((MyGame)game).IncreaseScore();
                ((MyGame)game).StartCombo();
                ((MyGame)game).ResetComboTime();
                ((MyGame)game).IncreaseKills();

                ((MyGame)game).SpawnScore();
                ((MyGame)game).scorehud.UpdateScoreOnCar(collidingObject as Enemy);

                collidingObject.LateDestroy();
            }

            if (collidingObject is House)
            {
                //SetXY(400, 400);
            }
            if (collidingObject is Obstacle)
            {
                ((MyGame)game).DecreaseOb();
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