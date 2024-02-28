using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ScoreHUD : GameObject
{
    MyGame mygame;
    
    EasyDraw displayScoreOnCar;
    
    public ScoreHUD (MyGame game)
    {

        this.mygame = game;
        DisplayScoreOnCar();

    }


    void DisplayScoreOnCar()
    {

        displayScoreOnCar = new EasyDraw(1366, 768, false);
        displayScoreOnCar.TextSize(50);
        displayScoreOnCar.Fill(Color.White);
        AddChild(displayScoreOnCar);
        

    }

    public void UpdateScoreOnCar(Enemy enemy)
    {
        displayScoreOnCar.ClearTransparent();
        displayScoreOnCar.y = displayScoreOnCar.y - 0.3f;
        
        displayScoreOnCar.Text("+" + mygame.scoreincrease, enemy.x-40, enemy.y);


    }

    void Update()
    {

        displayScoreOnCar.y = displayScoreOnCar.y - 0.3f;
        if (displayScoreOnCar.alpha < 256)
        {
            displayScoreOnCar.alpha = displayScoreOnCar.alpha + 8;
        }
        if (displayScoreOnCar.alpha > 240)
        {
            displayScoreOnCar.LateDestroy();
        }
    }



}