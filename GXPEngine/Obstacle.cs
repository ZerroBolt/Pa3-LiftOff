using GXPEngine;
using GXPEngine.Core;
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

        Vector2 position = setRandomPosition();
        SetXY(position.x, position.y);

        CheckColliding();
    }

    public void DestroyObstacle()
    {
        this.LateDestroy();
    }

    private Vector2 setRandomPosition()
    {
        float xPos = Utils.Random(0 + this.width, game.width);
        float yPos = Utils.Random(0 + this.height, game.height - this.height);

        return new Vector2(xPos, yPos);
    }

    void CheckColliding()
    {
        GameObject[] collidingObjects = GetCollisions();
        foreach (GameObject collidingObject in collidingObjects)
        {
            if (collidingObject is House || collidingObject is Player)
            {
                Vector2 position = setRandomPosition();
                SetXY(position.x, position.y);
                CheckColliding();
                break;
            }
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
