using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ScoreBoard : EasyDraw
{
    //TODO: Fill in hour registration school: 4,5 hours

    List<Player> playerList;

    public ScoreBoard(int gameWidth, int gameHeight, List<Player> players) : base(gameWidth, gameHeight, false)
    {
        playerList = players;
        ShowHighScores();
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

    void ShowNameInput()
    {
        //TODO: Show a screen were the player can input their high score name
    }

    void Update()
    {
        //TODO: when key is turned in score screen restart game
        if (Input.GetKeyDown(Key.R) && Input.GetKey(Key.LEFT_SHIFT))
        {
            ((MyGame)game).Restart();
        }
    }
}
