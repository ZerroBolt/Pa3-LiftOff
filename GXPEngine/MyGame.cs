using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	Sprite sp;
    public MyGame() : base(1366, 768, false)     // Arcade screen is 1366 x 768 pixels
	{
		Level level = new Level("Backgroundtest.tmx");

    targetFps = 60;

    AddChild(level);

		sp = new Sprite("square.png");
		sp.SetOrigin(sp.width / 2, sp.height / 2);
		sp.SetXY(width / 2, height / 2);
		AddChild(sp);

		EnemyController ec = new EnemyController(sp);
		AddChild(ec);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		if (Input.GetKey(Key.P))
        {
            Console.WriteLine(GetDiagnostics());
        }
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}