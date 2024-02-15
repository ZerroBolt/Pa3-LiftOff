using GXPEngine;
using System;
using System.Drawing;
using System.Threading;

public class HUD : GameObject
{
    EasyDraw displayHealth;
    EasyDraw displayScore;
    MyGame mygame;

    public HUD(MyGame game)
    {
        this.mygame = game;
        DisplayHealthHUD();
        DisplayScoreHUD();
        
    }



    //HEALTH
    void DisplayHealthHUD()
    {
        displayHealth = new EasyDraw(1366,768,false);
        displayHealth.TextSize(40);
        displayHealth.Fill(Color.Green);
        AddChild(displayHealth);
        

    }

    void UpdateHealth()
    {
        
        displayHealth.ClearTransparent();
        displayHealth.Text("Health:" + mygame.hp.ToString(), 640,100);
  
    }


    void DisplayScoreHUD()
    {
        displayScore = new EasyDraw(1366, 768, false);
        displayScore.TextSize(40);
        displayScore.Fill(Color.Yellow);
        AddChild(displayScore);
    }

    void UpdateScore()
    {
        displayScore.ClearTransparent();
        displayScore.Text("Score:" + mygame.score.ToString(), 300, 100);

    }

    void Update()
    {
        UpdateScore();
        UpdateHealth();

        if (mygame.hp < 4)
        {
            displayHealth.Fill(Color.Red);
        }
    }
}