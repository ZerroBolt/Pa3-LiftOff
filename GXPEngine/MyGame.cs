using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO.Ports;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
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
    
	public static bool isPortOpen = true;
    
	ObstacleController oc;
    LightningController lc;
    HUD hud;

    bool gameOver = false;
    bool scoreBoardShowing = false;

    List<Player> playerList = new List<Player>();

    Level level = null;

    public Camera cam;

    static ArduinoInput arduinoInput;

    public MyGame() : base(1366, 768, false, false)     // Arcade screen is 1366 x 768 pixels
	{
        targetFps = 60;

        //arduinoInput.SubscribeToStepEvent();

        Initialize();
    }

    public void AddPlayerToList(Player player)
    {
        playerList.Add(player);
    }

    void Initialize()
    {
        Sprite background = new Sprite("Background.png", false, false);
        background.SetOrigin(background.width/2, background.height/2);
        background.SetXY(game.width/2, game.height/2);
        background.scale = 1.2f;
        AddChild(background);

        cam = new Camera(0, 0, game.width, game.height);
        cam.scale = 0.8f;

        ec = new EnemyController();

        level = new Level("Backgroundtest.tmx", ec);
        AddChild(level);

        oc = new ObstacleController();
        AddChild(oc);

        AddChild(ec);

        lc = new LightningController();
        AddChild(lc);

        hud = new HUD(this);
        cam.AddChild(hud);

        Sound music = new Sound("music.mp3", true, true);

        music.Play(false, 0, 1);

        AddChild(cam);

        if (isPortOpen) ArduinoInput.SubscribeToStepEvent();
    }

	public void DecreaseHealth(int damage=0)
	{
        if (damage > 0) hp -= damage;
		else hp--;
        if (hp <= 0)
        {
            gameOver = true;
        }
	}
    public void IncreaseScore(bool isDrifting)
	{
        int defaultScore = 1000;
        if (isDrifting) defaultScore = (int)(defaultScore * 1.5f); 
        if (combo == 0)
        {
            score = score + defaultScore;
        }
        else
        {
            scoreincrease = defaultScore * combo;

            score = scoreincrease + score;
        }
        level.GetCurrentPlayer().score = score;
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

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		if (Input.GetKey(Key.P))
		{
			Console.WriteLine(GetDiagnostics());
        }

        if (Input.GetKeyDown(Key.E) && Input.GetKey(Key.LEFT_SHIFT))
        {
            gameOver = true;
        }
        
        if (combotime < Time.time)
        {
            ResetCombo();
        }
        combohudtime = (combotime - Time.time);

        CheckGameOver();

        MoveCamera();

        //Console.WriteLine("X: " + level.GetCurrentPlayer().x + "Y: " + level.GetCurrentPlayer().y);
    }

    public ScoreHUD scorehud;
    public void SpawnScore()
    {
        scorehud = new ScoreHUD(this);
        AddChild(scorehud);
    }
    public void DecreaseOb()
    {
        obstaclecount--;
    }

    private void MoveCamera()
    {
        Player currentPlayer = level.GetCurrentPlayer();
        float screenLimit = 200 / cam.scale;

        // Move screen on horizontal axis when car is close to the sides of the screen
        if (currentPlayer.x > 0 && currentPlayer.x < game.width)
        {
            if (currentPlayer.x < screenLimit)
            {
                cam.SetXY(currentPlayer.x + game.width / 2 - screenLimit, cam.y);
            }
            else if (currentPlayer.x > game.width - screenLimit)
            {
                cam.SetXY(currentPlayer.x - game.width / 2 + screenLimit, cam.y);
            }
        }

        // Move screen on vertical axis when car is close to the sides of the screen
        if (currentPlayer.y > (0 + currentPlayer.height) && currentPlayer.y < (game.height - currentPlayer.height))
        {
            if (currentPlayer.y < screenLimit)
            {
                cam.SetXY(cam.x, currentPlayer.y + game.height / 2 - screenLimit);
            }
            else if (currentPlayer.y > game.height - screenLimit)
            {
                cam.SetXY(cam.x, currentPlayer.y - game.height / 2 + screenLimit);
            }
        }
    }

    void CheckGameOver()
    {
        //Show Game over screen
        if (gameOver)
        {
            // Show scoreboard when GameOver and scoreBoard isn't already showing
            if (!scoreBoardShowing)
            {
                ScoreBoard sb = new ScoreBoard(game.width, game.height, playerList);
                cam.AddChild(sb);
                scoreBoardShowing = true;

                ec.Remove();
                oc.Remove();
                hud.Remove();
            }
        }
    }

    // Destroy all objects and create them again to restart the game
    public void Restart()
    {
        DestroyAll();
        Initialize();
    }

    // Destroy all GameObjects and reset variables
    private void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.LateDestroy();
        }

        hp = 10;
        score = 0;
        gameOver = false;
        scoreBoardShowing = false;
        level = null;
    }

    static void Main()                          // Main() is the first method that's called when the program is run
	{
        // Open port to read values from arduino
		if (isPortOpen)
		{
            arduinoInput = new ArduinoInput();
        }

        new MyGame().Start();                   // Create a "MyGame" and start it
	}
}