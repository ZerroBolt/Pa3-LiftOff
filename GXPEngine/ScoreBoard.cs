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

        // Sort the playerlist based on score and take the top 10
        Player[] top10Scores = playerList.OrderByDescending(p => p.score).Take(10).ToArray();

        for (int i = 0; i < top10Scores.Length; i++)
        {
            Player player = top10Scores[i];

            EasyDraw scoreDraw = new EasyDraw(game.width, 100, false);
            scoreDraw.SetXY(0, i * 50 + 100);
            scoreDraw.TextSize(30);
            scoreDraw.TextAlign(CenterMode.Center, CenterMode.Center);

            // If the player has a name show the name otherwise just show the playerNumber based on the index in the playerList
            if (player.playerName != null)
            {
                scoreDraw.Text(player.playerName + ", score: " + player.score.ToString(), scoreDraw.width / 2, scoreDraw.height / 2);
            }
            else scoreDraw.Text("Player #" + (playerList.IndexOf(player) + 1) + ", score: " + player.score.ToString(), scoreDraw.width / 2, scoreDraw.height / 2);

            AddChild(scoreDraw);
        }
    }

    void CreateBackground()
    {
        EasyDraw background = new EasyDraw(game.width, game.height, false);
        background.Rect(game.width / 2, game.height / 2, background.width, background.height);
        background.SetColor(0, 0, 0);
        background.alpha = 0.5f;

        AddChild(background);
    }

    EasyDraw nameInput;
    void ShowNameInput()
    {
        //TODO: Show a screen were the player can input their high score name
        this.ClearTransparent();
        CreateBackground();

        nameInput = new EasyDraw(game.width, game.height, false);
        nameInput.TextSize(30);
        nameInput.TextAlign(CenterMode.Center, CenterMode.Center);
        nameInput.Text("A");

        AddChild(nameInput);
    }

    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    char currentLetter = 'A';
    String pName = "";
    int nameLength = 3;

    void Update()
    {
        //TODO: when key is turned in score screen restart game
        if (MyGame.isPortOpen && ArduinoInput.GetControllerKey() && PlayerNameMade)
        {
            ((MyGame)game).Restart();
            PlayerNameMade = false;
        }

        if (Input.GetKeyDown(Key.R) && Input.GetKey(Key.LEFT_SHIFT))
        {
            ((MyGame)game).Restart();
        }

        InputNameLetters();
    }

    bool PlayerNameMade = false;
    void InputNameLetters()
    {
        if (nameInput != null && pName.Length < nameLength)
        {
            //TODO: Turn wheel / move slider to go to another letter
            if (Input.GetKeyDown(Key.UP))
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
            }
            else if (Input.GetKeyDown(Key.DOWN))
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

                ShowHighScores();
            }
        }
    }
}
