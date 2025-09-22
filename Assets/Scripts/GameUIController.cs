using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ScoreTextController scoreTextPlayer1, scoreTextPlayer2;
    public GameObject gameMenu;
    public Ball_Controller ball;
    public TextMeshProUGUI winText;

    public void UpdateScoreBoard(int scoreOfPlayer1, int scoreOfPlayer2)
    {
        scoreTextPlayer1.SetScore(scoreOfPlayer1);
        scoreTextPlayer2.SetScore(scoreOfPlayer2);
    }

    public void OnStartButtonClicked()
    {
        gameMenu.SetActive(false);
        scoreTextPlayer1.SetScore(0);
        scoreTextPlayer2.SetScore(0);
        ball.ResetBall();
        ball.Serve();
        Invoke("Serve", 1);
    }

    public void OnWin(int winnerId)
    {
        gameMenu.SetActive(true);
        winText.text = $"Player {winnerId} Victory";
    }
}
