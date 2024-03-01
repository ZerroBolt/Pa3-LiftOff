using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO.Ports;                           // System.Drawing contains drawing tools such as Color definitions
using System.Media;

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

    EasyDraw hbar;
    int maxHealth = 10;

    int healthBarWidth = 200;
    int healthBarHeight = 20;

    bool gameOver = false;
    bool scoreBoardShowing = false;
    bool isPlayingMusic = false;

    List<Player> playerList = new List<Player>();

    Level level = null;

    public Camera cam;

    bool startGame = true;

    public MyGame() : base(1366, 768, false, false)     // Arcade screen is 1366 x 768 pixels
    {
        targetFps = 60;

        Initialize();
    }

    public void AddPlayerToList(Player player)
    {
        playerList.Add(player);
    }

    void CreateBackGround()
    {
        Sprite background = new Sprite("Background.png", false, false);
        background.SetOrigin(background.width / 2, background.height / 2);
        background.SetXY(game.width / 2, game.height / 2);
        background.scale = 1.2f;
        AddChild(background);
    }

    void Initialize()
    {
        CreateBackGround();

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

        if (!isPlayingMusic)
        {
            Sound music = new Sound("music.wav", true, true);

            music.Play(false, 0, 1);

            isPlayingMusic = true;
        }

        hbar = new EasyDraw(1366, 768, false);


        // Draw health bar background
        hbar.Fill(200, 200, 200);
        hbar.Rect(100, 100, healthBarWidth, healthBarHeight);

        // Draw health bar foreground
        hbar.Fill(255, 0, 0);
        float healthRatio = (float)hp / maxHealth;
        hbar.Rect(100, 100, healthBarWidth * healthRatio, healthBarHeight);
        AddChild(hbar);

        AddChild(cam);

        if (isPortOpen) ArduinoInput.SubscribeToStepEvent();
    }

    public void DecreaseHealth(int damage = 0)
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
        if (startGame)
        {
            ScoreBoard sb = new ScoreBoard(game.width, game.height, playerList, true);
            cam.AddChild(sb);
            sb.ShowHighScores();
            scoreBoardShowing = true;

            ec.Remove();
            oc.Remove();
            hud.Remove();
            level.Remove();
            hbar.Remove();
            lc.Remove();
            startGame = false;
        }

        UpdateHealthbar();

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

    void UpdateHealthbar()
    {
        float healthRatio = (float)hp / maxHealth;
        hbar.ClearTransparent();
        hbar.Rect(game.width / 2, game.height / 2 + 140, healthBarWidth * healthRatio, healthBarHeight);
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
                sb.ShowNameInput();
                scoreBoardShowing = true;

                ec.Remove();
                oc.Remove();
                hud.Remove();
                level.Remove();
                hbar.Remove();
                lc.Remove();
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
            new ArduinoInput();
        }

        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}