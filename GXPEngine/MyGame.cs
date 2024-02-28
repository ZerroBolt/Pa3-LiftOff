using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO.Ports;                           // System.Drawing contains drawing tools such as Color definitions
using System.Media;

public class MyGame : Game
{
	/*Sprite sp;*/
	public int hp = 10;
	public int score = 0;
    public int scoreincrease = 0;
    public int combo = 1;
    public int combodisplay = 0;
    public float combohudtime = 0.000f;
    public int combotime = 0;
    public int kills = 0;
    int combodurationMs = 2000;

    public int obstaclecount = 0;

	EnemyController ec;

	static SerialPort port;

	static bool isPortOpen = false;
	ObstacleController oc;
    LightningController lc;

    
    public MyGame() : base(1366, 768, false, false)     // Arcade screen is 1366 x 768 pixels
    {
        //TODO: delete demo background
        Sprite background = new Sprite("BgDemo.png", false, false);
        AddChild(background);


        ec = new EnemyController();
        Level level = new Level("Backgroundtest.tmx", ec);

        targetFps = 60;

        AddChild(level);


        //      sp = new Sprite("square.png");
        //sp.SetOrigin(sp.width / 2, sp.height / 2);
        //sp.SetXY(width / 2, height / 2);
        //AddChild(sp);
        
        AddChild(ec);
        oc = new ObstacleController();
        AddChild(oc);
        lc = new LightningController();
        AddChild(lc);

        

        HUD hud = new HUD(this);
        AddChild(hud);

        Sound music = new Sound("music.mp3", true,true);

        music.Play(false, 0, 1);
    }


	public void DecreaseHealth()
	{
		hp--;
	}

    
	public void IncreaseScore()
	{
        if (combo == 0)
        {
            score = score + 1000;
        }
        else
        {
            scoreincrease =  1000 * combo;

            score = scoreincrease + score;
        }
	}

    public void IncreaseKills()
    {

        kills++;
    }

    public void StartCombo()
    {

        combo++;
        combodisplay++;
       
    }

    public void ResetComboTime()
    {
        combotime = Time.time + combodurationMs;

        combodurationMs = (int)(combodurationMs * 0.99f);

        
    }
    public void ResetCombo()
    {
        
            combo = 1;
            combodisplay = 0;
        combodurationMs = 2000;
    }
    public ScoreHUD scorehud;
    public void SpawnScore()
    {
        scorehud = new ScoreHUD(this);
        AddChild(scorehud);

        
    }






    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
       
        if (Input.GetKey(Key.P))
		{
       

            Console.WriteLine(GetDiagnostics());
		}

        if (combotime < Time.time)
        {
            ResetCombo();
        }
        combohudtime = (combotime - Time.time);

        
    }

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		if (isPortOpen)
		{
            port = new SerialPort();
            port.PortName = "COM11";
            port.BaudRate = 19200;
            port.RtsEnable = true;
            port.DtrEnable = true;
            port.Open();
            //while (true)
            //{
            //    string line = port.ReadLine(); // read separated values
            //    string line = port.ReadExisting(); // when using characters
            //    if (line != "")
            //    {
            //        Console.WriteLine("Read from port: " + line);

            //    }

            //    if (Console.KeyAvailable)
            //    {
            //        ConsoleKeyInfo key = Console.ReadKey();
            //        port.Write(key.KeyChar.ToString());  // writing a string to Arduino
            //    }
            //}
        }

        new MyGame().Start();                   // Create a "MyGame" and start it
	}
}