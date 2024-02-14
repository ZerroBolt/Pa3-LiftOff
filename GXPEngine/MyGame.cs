using System;                   // System contains a lot of default C# libraries 
using GXPEngine;                // GXPEngine contains the engine

public class MyGame : Game
{
    


    public MyGame() : base(800, 600, false)
    {


        Level level = new Level("Backgroundtest.tmx");

        targetFps = 60;

        AddChild(level);
        
    }

    void Update()
    {


        if (Input.GetKey(Key.P))
        {
            Console.WriteLine(GetDiagnostics());
        }
    }


    static void Main()
    {

        new MyGame().Start();
    }
}