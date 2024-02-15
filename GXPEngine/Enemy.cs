using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

class Enemy : AnimationSprite
{
    const string imageLocation = "barry.png";

    float speed = 1f;
    int health = 1;

    float animSpeed = 0.25f;
    Sprite target;

    int _damage = 1;
    public int damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public Enemy(TiledObject obj=null) : base(imageLocation, 7, 1)
    {
        Initialize(obj);
    }

    private void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        SetCycle(0, 4);
        collider.isTrigger = true;

        if (obj != null)
        {
            speed = obj.GetFloatProperty("speed", 1f);
            health = obj.GetIntProperty("health", 1);
            damage = obj.GetIntProperty("damage", 1);
        }

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
    }

    public void SetTarget(Sprite target)
    {
        this.target = target;
    }

    private void Update()
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

            if (dx < 0)
            {
                Mirror(true, false);
            }
            else
            {
                Mirror(false, false);
            }
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
        }

        Animate(animSpeed);
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
}
