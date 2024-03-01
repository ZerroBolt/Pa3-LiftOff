using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Enemy : AnimationSprite
{
    float animSpeed = 0.25f;
    Sprite target;

    float _speed = 0.5f;
    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    int maxHealth = 1;

    int _health = 1;
    public int health
    {
        get { return _health; }
        set 
        { 
            _health = value;
            if (_health > 0)
            {
                float percentage = 1f * _health / maxHealth;
                SetColor(1, percentage, percentage);
            }
        }
    }

    int _damage = 1;
    public int damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public Enemy(string filename, int cols, int rows, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, frames, keepInCache, addCollider)
    {
        Initialize();
    }

    Sound hithouse = new Sound("house_damage.wav");
    Sound spawning1 = new Sound("zombie_spawning1.wav");
    Sound spawning2 = new Sound("zombie_spawning2.wav");
    Sound spawning3 = new Sound("zombie_spawning3.wav");

    protected void Initialize()
    {
        SetOrigin(width / 2, height / 2);

        maxHealth = health;
        
        collider.isTrigger = true;

        setRandomPosition();
    }

    private void setRandomPosition()
    {
        //Set enemy position randomly on TOP,RIGHT,DOWN,LEFT outside of screen
        switch (Utils.Random(0, 4))
        {
            case 0:
                //TOP
                x = Utils.Random(0 - this.width, game.width + this.width);
                y = Utils.Random(0 - this.height, 0);
 
                break;
            case 1:
                //RIGHT
                x = Utils.Random(game.width, game.width + this.width);
                y = Utils.Random(0 - this.height, game.height + this.height);
                break;
            case 2:
                //DOWN
                x = Utils.Random(0 - this.width, game.width + this.width);
                y = Utils.Random(game.height, game.height + this.height);
                break;
            case 3:
                //LEFT
                x = Utils.Random(0 - this.width, 0);
                y = Utils.Random(0 - this.height, game.height + this.height);
                break;
        }
        switch (Utils.Random(0, 30))
        {
            case 0: spawning1.Play(); break;
            case 1: spawning2.Play(); break;
            case 2: spawning3.Play(); break;
        }
    }

    public void SetTarget(Sprite target)
    {
        this.target = target;
    }

    protected void Update()
    {
        if (this.target == null) return;

        float dx = 0;
        float dy = 0;

        if (x != target.x)
        {
            if (x < target.x)
            {
                dx += speed * CalculateAngle().x;
            }
            else
            {
                dx -= speed * CalculateAngle().x;
            }

            //if (dx < 0)
            //{
            //    Mirror(false, false);
            //}
            //else
            //{
            //    Mirror(true, false);
            //}
        }

        if (y < target.y)
        {
            dy += speed * CalculateAngle().y;
        }
        else
        {
            dy -= speed * CalculateAngle().y;
        }

        Move(dx, dy);

        //TODO: Check for collision
        //Do damage when it is the house
        if (HitTest(target))
        {
            this.Destroy();
            ((MyGame)game).DecreaseHealth(damage);
            hithouse.Play();
        }

        //Animate(animSpeed);
    }

    Vector2 CalculateAngle()
    {
        //Y difference between enemy and target
        float yVal = Math.Abs(this.y - target.y);

        //X difference between enemy and target
        float xVal = Math.Abs(this.x - target.x);

        //Calculate to the power of x,y values
        float yDifference = Mathf.Pow(yVal, 2);
        float xDifference = Mathf.Pow(xVal, 2);
        float xyVal = yDifference + xDifference;

        //Calculate square root of value
        float sqrtVal = Mathf.Sqrt(xyVal);

        //Return Vector2 with calculate x,y value devided by square root
        return new Vector2(xVal / sqrtVal, yVal / sqrtVal);
    }

    int invincibleDurationMs = 1000;
    int invincibleTime = 0;
    public bool IsInvincible()
    {
        if (Time.time > invincibleTime)
        {
            invincibleTime = Time.time + invincibleDurationMs;
            return false;
        }

        return true;
    }
}
