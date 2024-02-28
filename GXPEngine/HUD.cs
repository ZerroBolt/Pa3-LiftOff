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
        this.SetXY(-game.width/2, -game.height/2);
        this.mygame = game;
        DisplayHealthHUD();
        DisplayScoreHUD();
    }

    //HEALTH
    void DisplayHealthHUD()
    {
        displayHealth = new EasyDraw(1366, 768, false);
        displayHealth.TextSize(30);
        displayHealth.TextAlign(CenterMode.Center, CenterMode.Center);
        displayHealth.Fill(Color.Blue);
        ((MyGame)game).AddChild(displayHealth);
    }

    void UpdateHealth()
    {
        displayHealth.ClearTransparent();

        displayHealth.Text("Health:" + mygame.hp.ToString(), game.width / 2, game.height / 2 + 150);
    }

    //SCORE
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
        displayScore.Text("Score:" + mygame.score.ToString(), game.width / 2 - 120, 100);
    }

    /*
    void DisplayComboHUD()
    {
        displayCombo = new EasyDraw(1366, 768, false);
        displayCombo.TextSize(20);
        
        displayCombo.Fill(Color.White);
        AddChild(displayCombo); 
    }

    void UpdateCombo()
    {
        displayCombo.ClearTransparent();
        displayCombo.Text("Combo:" + mygame.combodisplay.ToString(), 100,100);
    }

    void DisplayComboTimeHUD()
    {
        displayComboTime = new EasyDraw(1366, 768, false);
        displayComboTime.TextSize(20);
        displayComboTime.Fill(Color.White);
        AddChild(displayComboTime);
    }

    void UpdateComboTime()
    {
        displayComboTime.ClearTransparent();
        if (mygame.combotime >= Time.time)
        {
            
            displayComboTime.Text("Combo time left:" + mygame.combohudtime , 100, 200);
        }
    }
    */

    /*
    void DisplayKillsHUD()
    {
        displayKills = new EasyDraw(1366, 768, false);
        displayKills.TextSize(20);
        displayKills.Fill(Color.Black);
        AddChild(displayKills);
    }
    */

    /*    
    void UpdateKills()
    {
        displayKills.ClearTransparent();
        displayKills.Text("Kills:"+ mygame.kills.ToString(), 1100, 100);

    }
    */

    void Update()
    {
        //UPDATE HUDS
        UpdateScore();
        UpdateHealth();

        /*
        UpdateCombo();
        UpdateComboTime();
        */

        //UpdateKills();

        if (mygame.hp < 4)
        {
            displayHealth.Fill(Color.Red);
        }
    }
}