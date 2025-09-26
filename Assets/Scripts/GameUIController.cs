using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ScoreTextController scoreTextPlayer1, scoreTextPlayer2;
    public GameObject gameMenu;
    public Ball_Controller ball;
    public TMPro.TextMeshProUGUI winText;

    private void OnEnable()
    {
        // Subscribe to score and win events (Observer pattern)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScore += UpdateScoreBoard;
            GameManager.Instance.OnWin += ShowWinScreen;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScore -= UpdateScoreBoard;
            GameManager.Instance.OnWin -= ShowWinScreen;
        }
    }


    private void UpdateScoreBoard(int scoreOfPlayer1, int scoreOfPlayer2)
    {
        scoreTextPlayer1.SetScore(scoreOfPlayer1);
        scoreTextPlayer2.SetScore(scoreOfPlayer2);
    }

    private void ShowWinScreen(int winnerId)
    {
        ball.ResetBall();
        gameMenu.SetActive(true);
        winText.text = $"Player {winnerId} Victory";
    }

    public void OnStartButtonClicked()
    {
        gameMenu.SetActive(false);

        // Reset GameManager scores for the new game
        GameManager.Instance.ResetScores();


        ball.ballSpeed = ball.baseBallSpeed;
        ball.ResetBall();
        ball.Serve();
    }
}
