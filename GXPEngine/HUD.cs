using GXPEngine;
using System;
using System.Drawing;
using System.Threading;

public class HUD : GameObject
{
    EasyDraw displayHealth;
    MyGame mygame;

    public HUD(MyGame game)
    {
        this.mygame = game;
        DisplayHealthHUD();
        
    }



    //HEALTH
    void DisplayHealthHUD()
    {
        displayHealth = new EasyDraw(1366,768,true);
        displayHealth.TextSize(40);
        AddChild(displayHealth);

    }

    void UpdateHealth()
    {
        
        displayHealth.ClearTransparent();
        displayHealth.Text("Health:" + mygame.hp.ToString(), 640,100);
  
    }





    void Update()
    {
        
        UpdateHealth();
    }
}