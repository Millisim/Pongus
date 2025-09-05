using UnityEngine;

public class GameManager : MonoBehaviour
{
    int scoreOfPlayer1, scoreOfPlayer2;
    public ScoreTextController scoreTextLeft, scoreTextRight;
    
    public void SetScores(string zoneTag)
    {
        if (zoneTag == "L-Zone")
            scoreOfPlayer2++;

        if (zoneTag == "R-Zone")
            scoreOfPlayer1++;

        UpdateScoreBoard();
    }

    private void UpdateScoreBoard()
    {
        scoreTextLeft.SetScore(scoreOfPlayer1);
        scoreTextRight.SetScore(scoreOfPlayer2);
    }
}
