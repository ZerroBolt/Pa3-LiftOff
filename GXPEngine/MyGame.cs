using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;
using System.Xml.Serialization;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
	Sprite sp;
	public int hp = 10;
	public int score = 0;
	EnemyController ec;
	public MyGame() : base(1366, 768, false, false)     // Arcade screen is 1366 x 768 pixels
	{
		Level level = new Level("Backgroundtest.tmx");

		targetFps = 60;

		AddChild(level);
        HUD hud = new HUD(this);
        AddChild(hud);

        sp = new Sprite("square.png");
		sp.SetOrigin(sp.width / 2, sp.height / 2);
		sp.SetXY(width / 2, height / 2);
		AddChild(sp);

		ec = new EnemyController();
		ec.SetTarget(sp);
		AddChild(ec);
	}

	public void DecreaseHealth()
	{
		hp--;
	}
	public void IncreaseScore()
	{
		score = score + 100;
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
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}