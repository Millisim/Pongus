using UnityEngine;

public class GameManager : MonoBehaviour
{
    int scoreOfPlayer1, scoreOfPlayer2;
    // public ScoreTextController scoreTextLeft, scoreTextRight;
    public GameUIController gameUI;
    int winScore = 4;

    public void SetScores(string zoneTag)
    {
        if (zoneTag == "L-Zone")
            scoreOfPlayer2++;

        if (zoneTag == "R-Zone")
            scoreOfPlayer1++;

        gameUI.UpdateScoreBoard(scoreOfPlayer1, scoreOfPlayer2);
    }

    public bool CheckWin()
    {
        int winnerId = scoreOfPlayer1 == winScore ? 1 : scoreOfPlayer2 == winScore ? 2 : 0;
        if (winnerId != 0)
        {
            gameUI.OnWin(winnerId);
            return true;
        }
        return false;
    }
}