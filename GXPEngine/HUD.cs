using GXPEngine;
using System;
using System.Drawing;
using System.Threading;
using TiledMapParser;

public class HUD : GameObject
{
    EasyDraw displayHealth;
    EasyDraw displayScore;
    /* EasyDraw displayCombo;
     EasyDraw displayComboTime;
     EasyDraw displayKills;*/
   
    MyGame mygame;
    
    public HUD(MyGame game)
    {
        this.mygame = game;
        DisplayHealthHUD();
        DisplayScoreHUD();
        /*DisplayComboHUD();
        DisplayComboTimeHUD();*/
        /*DisplayKillsHUD();*/

        
        
        
    }



    //HEALTH
    void DisplayHealthHUD()
    {
        displayHealth = new EasyDraw(1366,768,false);
        displayHealth.TextSize(40);
        displayHealth.Fill(Color.Blue);
        AddChild(displayHealth);
        

    }

    void UpdateHealth()
    {
        
        displayHealth.ClearTransparent();
        
        displayHealth.Text("Health:" + mygame.hp.ToString(), game.width/2-120,game.height/2+200);
  
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
        displayScore.Text("Score:" + mygame.score.ToString(), game.width/2-120, 100);

    }

/*    void DisplayComboHUD()
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
    }*/

/*    void DisplayKillsHUD()
    {
        displayKills = new EasyDraw(1366, 768, false);
        displayKills.TextSize(20);
        displayKills.Fill(Color.Black);
        AddChild(displayKills);
    }*/

/*    void UpdateKills()
    {
        displayKills.ClearTransparent();
        displayKills.Text("Kills:"+ mygame.kills.ToString(), 1100, 100);
        
    }*/
 
   
    void Update()
    {
        //UPDATE HUDS
        UpdateScore();
        UpdateHealth();
        /*UpdateCombo();
        UpdateComboTime();*/
        //UpdateKills();
        
        

        if (mygame.hp < 4)
        {
            displayHealth.Fill(Color.Red);
        }
    }
}