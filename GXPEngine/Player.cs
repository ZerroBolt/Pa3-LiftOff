using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Player : AnimationSprite
{
    float turnSpeedTruck = 1;
    float maxSpeed = 5;
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

    const string fileLocation = "CarSpritesheet.png";
    AnimationSprite car;

    float originalSpeed = 0;

    public Player(string fileName, int cols, int rows, TiledObject tiledobject = null) : base(fileName, cols, rows)
    {

    }


    public Player(TiledObject obj = null) : base(fileLocation, 4, 1)
    {
        this.alpha = 0;
        SetFrame(0);
        car = new AnimationSprite(fileLocation, 4, 1);
        car.SetOrigin(car.width / 2, car.height / 2);
        ((MyGame)game).AddChild(car);
    }

    bool isDrifting = false;
    void RotateTruck()
    {
        if (MyGame.isPortOpen)
        {
            //TODO: Rotate the truck based on the steering wheel rotation
            int wheelRotation = ArduinoInput.GetSteeringRotation();
            int shiftValue = ArduinoInput.GetShiftPosition();

            //Console.WriteLine(wheelRotation);

            // Rotation 0 - 30 && speed 91 - 100 = drift left
            if ((wheelRotation >= -140 && wheelRotation < -120) && (shiftValue > 92 && shiftValue <= 100))
            {
                // Drift left
                isDrifting = true;
                this.rotation += ((turnSpeedTruck / 240) * wheelRotation);
            }

            // Rotation 250 - 280 && speed 91 - 100 = drift right
            else if ((wheelRotation > 130 && wheelRotation <= 140) && (shiftValue > 92 && shiftValue <= 100))
            {
                // Drift right
                isDrifting = true;
                this.rotation += ((turnSpeedTruck / 240) * wheelRotation);
            }

            // Rotation 0 - 120 = rotate left
            else if (wheelRotation >= -140 && wheelRotation < -20)
            {
                this.rotation += ((turnSpeedTruck / 240) * wheelRotation);
                isDrifting = false;
            }

            // Rotation 160 - 280 = rotation right
            else if (wheelRotation > 20 && wheelRotation <= 140)
            {
                this.rotation += ((turnSpeedTruck / 240) * wheelRotation);
                isDrifting = false;
            }

            // Rotation 121 - 159 = move forward
            else
            {
                // Move forward
                isDrifting = false;
            }

        }
        else
        {
            if (Input.GetKey(Key.A))
            {
                rotation -= turnSpeedTruck;
            }

            if (Input.GetKey(Key.D))
            {
                rotation += turnSpeedTruck;
            }
        }
    }

    float SetMovingSpeed(float dx)
    {
        if (MyGame.isPortOpen)
        {
            int shiftValue = ArduinoInput.GetShiftPosition();
            //TODO: Move the truck based on the shift position
            // Shift 0 - 39 = backwards
            if (shiftValue < 25)
            {
                moveSpeedTruck = -2;
            }

            // Shift 40 - 90 = forwards
            if (shiftValue > 30 && shiftValue < 100)
            {
                moveSpeedTruck = (maxSpeed / 100) * shiftValue;
            }

            // Shift 91 - 100 = drift
        }

        return dx;
    }

    void MoveTruck()
    {
        car.SetXY(x, y);
        Moving = false;
        float dx = 0;
        float dy = 0;
        float oldx = x;
        float oldy = y;

        RotateTruck();

        //if (Input.GetKey(Key.W) && !Slowed)
        //{
        //    if (moveSpeedTruck < maxSpeed)
        //    {
        //        moveSpeedTruck += 0.5f;
        //    }
        //    originalSpeed = moveSpeedTruck;
        //}
        //else if (Input.GetKey(Key.S) && !Slowed)
        //{
        //    if (moveSpeedTruck > -3)
        //    {
        //        moveSpeedTruck -= 0.5f;
        //    }
        //    else moveSpeedTruck = -3f;
        //    originalSpeed = moveSpeedTruck;
        //}


        if (!Slowed) SetMovingSpeed(dx);

        Move(0, dx -= moveSpeedTruck);
        //Move(0, MoveTruck(dx));

        if (moveSpeedTruck != 0)
        {
            Moving = true;
        }

        int delaTimeClamped = Mathf.Min(Time.deltaTime, 40);

        float vx = dx * delaTimeClamped / 1000;
        float vy = dy * delaTimeClamped / 1000;

        MoveUntilCollision(vx, 0);
        MoveUntilCollision(0, vy);

        if (!isDrifting) SetMovingFrame(rotation);
        else SetMovingFrame(rotation, 90);

        GameObject[] collidingObjects = GetCollisions();
        foreach (GameObject collidingObject in collidingObjects)
        {
            if (collidingObject is Enemy && Moving)
            {
                Enemy enemy = collidingObject as Enemy;

                ((MyGame)game).StartCombo();
                ((MyGame)game).ResetComboTime();

                enemy.health--;

                if (enemy.IsInvincible()) break;

                if (enemy.health <= 0)
                {
                    ((MyGame)game).IncreaseScore(isDrifting);
                    ((MyGame)game).IncreaseKills();

                    ((MyGame)game).SpawnScore();
                    ((MyGame)game).scorehud.UpdateScoreOnCar(enemy);

                    collidingObject.LateDestroy();

                    switch (Utils.Random(0, 3))
                    {
                        case 0: dyingzombie1.Play(false, 0, 1); break;
                        case 1: dyingzombie2.Play(false, 0, 1); break;
                        case 2: dyingzombie3.Play(false, 0, 1); break;
                    }
                }
            }

            if (collidingObject is House)
            {
                //TODO: Fix moving through house
                //SetXY(oldx, oldy);
                
            }
            if (collidingObject is Obstacle)
            {
                hitobstacle.Play(false, 0, 0.5f);
                collidingObject.LateDestroy();
                Slowplayer();
            }
            /*  if (collidingObject is Lightning)
              {
                  collidingObject.Destroy();
                  Slowplayer();

              }*/
        }

        if (x < 0 - (car.width / 2) || x > game.width + (car.width / 2))
        {
            x = oldx;
        }
        if (y < 0 || y > game.height)
        {
            y = oldy;
        }
    }

    void SetMovingFrame(float rotation, int quarterRotation = 0)
    {
        float rotationDegree = (rotation % 360) + quarterRotation;
        if (rotationDegree < 0) rotationDegree = 360 - ((Math.Abs(rotation) % 360) - quarterRotation);

        Console.WriteLine(rotationDegree);

        if (rotationDegree > 30 && rotationDegree < 150)
        {
            car.SetFrame(3);
        }
        else if (rotationDegree > 150 && rotationDegree < 210)
        {
            car.SetFrame(1);
        }
        else if (rotationDegree > 210 && rotationDegree < 330)
        {
            car.SetFrame(2);
        }
        else if (rotationDegree < 30 || rotationDegree > 330)
        {
            car.SetFrame(0);
        }
    }

    public void Slowplayer()

    {
        originalSpeed = moveSpeedTruck;

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
            moveSpeedTruck = originalSpeed;
            turnSpeedTruck = 3;
        }

        if (Moving)
        {
            movingcar.Play(false, 0, 0);

            //Console.WriteLine("moving");
        }
        else
        {
            idlecar.Play(false, 0, 0);

            //Console.WriteLine("not moving");
        }
        //Console.WriteLine(Time.time);
    }
}