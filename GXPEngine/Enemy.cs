using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

class Enemy : AnimationSprite
{
    const string imageLocation = "barry.png";

    float speed = 0.5f;
    int health = 1;

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
        SetCycle(0, 7);
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
        //switch (Utils.Random(0, 2))
        //{
        //    case 0:
        //        x = Utils.Random(game.width, game.width + width);
        //        break;
        //    case 1:
        //        x = Utils.Random(0 - width, 0);
        //        break;
        //}

        //switch (Utils.Random(0, 2))
        //{
        //    case 0:
        //        y = Utils.Random(game.height, game.height + height);
        //        break;
        //    case 1:
        //        y = Utils.Random(0 - height, 0);
        //        break;
        //}

        x = Utils.Random(0 - this.width, game.width + this.width);
        y = Utils.Random(0 - this.height, game.height + this.height);
    }

    public void SetTarget(Sprite target)
    {
        this.target = target;
    }

    private void Update()
    {
        float oldX = x;
        //TODO: move from outside of screen to center of screen
        if (x < target.x)
        {
            x += speed;
        }
        else
        {
            x -= speed;
        }

        if (x < oldX)
        {
            Mirror(true, false);
        }
        else
        {
            Mirror(false, false);
        }

        if (y < target.y)
        {
            y += speed;
        }
        else
        {
            y -= speed;
        }

        //TODO: Check for collision
        //Do damage when it is the house
        if (HitTest(target))
        {
            this.Destroy();
        }

        Animate(0.25f);
    }
}
