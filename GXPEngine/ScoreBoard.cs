using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ScoreBoard : EasyDraw
{
    List<Player> playerList;

    public ScoreBoard(int gameWidth, int gameHeight, List<Player> players) : base(gameWidth, gameHeight, false)
    {
        playerList = players;
        //ShowHighScores();
        this.SetXY(-game.width/2, -game.height/2);
        ShowNameInput();
    }

    void ShowHighScores()
    {
        this.ClearTransparent();
        CreateBackground();

        // Create EasyDraw for title of the game
        EasyDraw titleDraw = new EasyDraw(game.width, game.height, false);
        titleDraw.TextSize(50);
        titleDraw.TextAlign(CenterMode.Center, CenterMode.Min);
        titleDraw.Text("Truck Pocalypse");
        titleDraw.y = 20;
        AddChild(titleDraw);

        // Sort the playerlist based on score and take the top 10
        Player[] top10Scores = playerList.OrderByDescending(p => p.score).Take(10).ToArray();

        // Create scorelist
        for (int i = 0; i < top10Scores.Length; i++)
        {
            Player player = top10Scores[i];

            EasyDraw scoreDraw = new EasyDraw(game.width, 100, false);
            scoreDraw.SetXY(0, i * 50 + 100);
            scoreDraw.TextSize(30);
            scoreDraw.TextAlign(CenterMode.Center, CenterMode.Center);
            scoreDraw.y += 20;

            // If the player has a name show the name otherwise just show the playerNumber based on the index in the playerList
            if (player.playerName != null)
            {
                scoreDraw.Text(player.playerName + ", score: " + player.score.ToString(), scoreDraw.width / 2, scoreDraw.height / 2);
            }
            else scoreDraw.Text("Player #" + (playerList.IndexOf(player) + 1) + ", score: " + player.score.ToString(), scoreDraw.width / 2, scoreDraw.height / 2);

            AddChild(scoreDraw);
        }

        // Create EasyDraw for turn key text
        EasyDraw turnKeyDraw = new EasyDraw(game.width, game.height, false);
        turnKeyDraw.TextSize(30);
        turnKeyDraw.TextAlign(CenterMode.Center, CenterMode.Max);
        turnKeyDraw.Text("Turn key to start game");
        turnKeyDraw.y -= 20;
        AddChild(turnKeyDraw);
    }

    void CreateBackground()
    {
        // Create background
        EasyDraw background = new EasyDraw(game.width, game.height, false);
        background.Rect(game.width / 2, game.height / 2, background.width, background.height);
        background.SetColor(0, 0, 0);
        background.alpha = 0.5f;
        AddChild(background);
    }

    EasyDraw nameInput;
    EasyDraw gameOverDraw;
    EasyDraw turnKeyDraw;
    void ShowNameInput()
    {
        //TODO: Show a screen were the player can input their high score name
        this.ClearTransparent();
        CreateBackground();

        // Create EasyDraw for game over text
        gameOverDraw = new EasyDraw(game.width, game.height, false);
        gameOverDraw.TextSize(50);
        gameOverDraw.TextAlign(CenterMode.Center, CenterMode.Min);
        gameOverDraw.Text("GAME OVER");
        gameOverDraw.y = 50;
        AddChild(gameOverDraw);

        // Create EasyDraw for name input
        nameInput = new EasyDraw(game.width, game.height, false);
        nameInput.TextSize(30);
        nameInput.TextAlign(CenterMode.Center, CenterMode.Center);
        nameInput.Text("A");
        AddChild(nameInput);

        // Create EasyDraw for turn key text
        turnKeyDraw = new EasyDraw(game.width, game.height, false);
        turnKeyDraw.TextSize(30);
        turnKeyDraw.TextAlign(CenterMode.Center, CenterMode.Max);
        turnKeyDraw.Text("Turn key to put in your name");
        turnKeyDraw.y -= 20;
        AddChild(turnKeyDraw);
    }

    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    char currentLetter = 'A';
    String pName = "";
    int nameLength = 3;

    int nameInputCoolDownMs = 200;
    int nameInputTime = 0;

    void Update()
    {
        //TODO: when key is turned in score screen restart game
        if (MyGame.isPortOpen && ArduinoInput.GetControllerKey() && PlayerNameMade && (Time.time > nameInputTime))
        {
            ((MyGame)game).Restart();
            PlayerNameMade = false;
        }

        if (Input.GetKeyDown(Key.R) && Input.GetKey(Key.LEFT_SHIFT))
        {
            ((MyGame)game).Restart();
        }

        //if (Time.time > nameInputTime)
        //{
        //    nameInputTime = Time.time + nameInputCoolDownMs;

        //    InputNameLetters();
        //}

        if (Time.time > nameInputTime)
        {
            InputNameLetters();
        }
    }

    int letterSwitchCoolDownMs = 300;
    int letterTime = 0;

    bool PlayerNameMade = false;
    void InputNameLetters()
    {
        if (nameInput != null && pName.Length < nameLength)
        {
            //TODO: Turn wheel / move slider to go to another letter
            if (Input.GetKeyDown(Key.UP) || (MyGame.isPortOpen && ArduinoInput.GetShiftPosition() > 70 && (Time.time > letterTime)))
            {
                char[] letters = alphabet.ToCharArray();
                for (int i = 0; i < letters.Length; i++)
                {
                    char letter = letters[i];
                    if (letter.Equals(currentLetter))
                    {
                        if (i + 1 < letters.Length) currentLetter = letters[i + 1];
                        break;
                    }
                }
                nameInput.ClearTransparent();
                nameInput.Text(pName + currentLetter);
                letterTime = Time.time + letterSwitchCoolDownMs;
            }
            else if (Input.GetKeyDown(Key.DOWN) || (MyGame.isPortOpen && ArduinoInput.GetShiftPosition() < 60 && (Time.time > letterTime)))
            {
                char[] letters = alphabet.ToCharArray();
                for (int i = letters.Length - 1; i > 0; i--)
                {
                    char letter = letters[i];
                    if (letter.Equals(currentLetter))
                    {
                        if (i - 1 < letters.Length) currentLetter = letters[i - 1];
                        break;
                    }
                }
                nameInput.ClearTransparent();
                nameInput.Text(pName + currentLetter);
                letterTime = Time.time + letterSwitchCoolDownMs;
            }
            //TODO: Turn key to go to next letter (test this)
            else if (Input.GetKeyDown(Key.ENTER) || (MyGame.isPortOpen && ArduinoInput.GetControllerKey()))
            {
                pName += currentLetter;
                if (pName.Length == nameLength)
                {
                    playerList.Last().playerName = pName;

                    return;
                }
                currentLetter = 'A';
                nameInput.ClearTransparent();
                nameInput.Text(pName + currentLetter);

                nameInputTime = Time.time + nameInputCoolDownMs;
            }
        }

        if (nameInput != null && pName.Length == nameLength)
        {
            //TODO: Turn key to input your name (test this)
            if (Input.GetKey(Key.ENTER) || (MyGame.isPortOpen && ArduinoInput.GetControllerKey()))
            {
                PlayerNameMade = true;

                if (nameInput != null)
                {
                    nameInput.Destroy();
                    nameInput = null;
                }

                if (gameOverDraw != null)
                {
                    gameOverDraw.Destroy();
                    gameOverDraw = null;
                }

                if (turnKeyDraw != null)
                {
                    turnKeyDraw.Destroy();
                    turnKeyDraw = null;
                }


                nameInputTime = Time.time + nameInputCoolDownMs;

                ShowHighScores();
            }
        }
    }
}
