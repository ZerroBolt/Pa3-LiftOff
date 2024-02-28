using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Obstacle : AnimationSprite
{

    public Obstacle(TiledObject obj = null) : base("stone1.png", 1, 1)
    {
        Initialize(obj);
    }

    int Despawntime = Time.time + 15000;
    private void Initialize(TiledObject obj)
    {
        SetOrigin(width / 2, height / 2);
        
        collider.isTrigger = true;
        scale = 0.25f;

        setRandomPosition();
    }

    public void DestroyObstacle()
    {
        this.LateDestroy();
    }

    private void setRandomPosition()
    {
        //Set enemy position randomly on TOP,RIGHT,DOWN,LEFT outside of screen
        switch (Utils.Random(0, 4))
        {
            case 0:
                //TOP
                x = Utils.Random(0 - this.width, game.width);
                y = Utils.Random(0 - this.height, game.width / 2);
                break;

            case 1:
                //RIGHT
                x = Utils.Random(game.width / 2, game.width);
                y = Utils.Random(0 + this.height, game.height - this.height);
                break;

            case 2:
                //DOWN
                x = Utils.Random(game.width / 2, game.width);
                y = Utils.Random(game.height / 2 + 100, game.height);
                break;

            case 3:
                //LEFT
                x = Utils.Random(0, game.width / 2 - 50);
                y = Utils.Random(0 - this.height, game.height + this.height);
                break;
        }
    }

    void Update()
    {
        if (Time.time > Despawntime)
        {
            Despawntime = Time.time + Despawntime;

            DestroyObstacle();
            Console.WriteLine("destroy lightning");
        }
    }
}
