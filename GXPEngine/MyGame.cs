using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO.Ports;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
	Sprite sp;
	public int hp = 10;
	public int score = 0;
	EnemyController ec;
<<<<<<< Updated upstream
	static SerialPort port;
=======
	ObstacleController oc;
  static SerialPort port;
>>>>>>> Stashed changes
	static bool isPortOpen = false;
	ObstacleController oc;

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

        oc = new ObstacleController();
        AddChild(oc);

        AddChild(ec);

        HUD hud = new HUD(this);
        AddChild(hud);
    }

	public void DecreaseHealth()
	{
		hp--;
	}
	public void IncreaseScore()
	{
		score = score + 1000;
	}

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		if (Input.GetKey(Key.P))
		{
			
			
			Console.WriteLine(GetDiagnostics());
		}

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