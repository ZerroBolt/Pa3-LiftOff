using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	Sprite sp;
    List<Enemy> enemyList = new List<Enemy>();
    public MyGame() : base(1366, 768, false)     // Arcade screen is 1366 x 768 pixels
	{
		//// Draw some things on a canvas:
		//EasyDraw canvas = new EasyDraw(800, 600);
		//canvas.Clear(Color.MediumPurple);
		//canvas.Fill(Color.Yellow);
		//canvas.Ellipse(width / 2, height / 2, 200, 200);
		//canvas.Fill(50);
		//canvas.TextSize(32);
		//canvas.TextAlign(CenterMode.Center, CenterMode.Center);
		//canvas.Text("Welcome!", width / 2, height / 2);

		//// Add the canvas to the engine to display it:
		//AddChild(canvas);
		//Console.WriteLine("MyGame initialized");

		sp = new Sprite("square.png");
		sp.SetOrigin(sp.width / 2, sp.height / 2);
		sp.SetXY(width / 2, height / 2);
		AddChild(sp);

		for (int i = 0; i < 10; i++)
		{
			Enemy enemy = new Enemy();
            enemy.SetTarget(sp);
            enemyList.Add(enemy);
            AddChild(enemy);
        }
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		// Empty
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}