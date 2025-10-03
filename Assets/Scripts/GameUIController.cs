using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ScoreTextController scoreTextPlayer1, scoreTextPlayer2;
    public GameObject gameMenu;
    public Ball_Controller ball;
    public TMPro.TextMeshProUGUI winText;
    public TMPro.TextMeshProUGUI playModeText;

    private void OnEnable()
    {
        // Subscribe to score and win events (Observer pattern)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScore += UpdateScoreBoard;
            GameManager.Instance.OnWin += ShowWinScreen;
        }
    }

    private void Start()
    {
        OnPlayModeButtonClicked();
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

    public void OnPlayModeButtonClicked()
    {
        switch (GameManager.Instance.playMode)
        {
            case GameManager.PlayMode.PlayerVsPlayer:
                GameManager.Instance.playMode = GameManager.PlayMode.PlayerVsCPU;
                playModeText.text = "Player vs CPU";
                break;

            case GameManager.PlayMode.PlayerVsCPU:
                GameManager.Instance.playMode = GameManager.PlayMode.CPUVsCPU;
                playModeText.text = "CPU vs CPU";
                break;

            case GameManager.PlayMode.CPUVsCPU:
                GameManager.Instance.playMode = GameManager.PlayMode.PlayerVsPlayer;
                playModeText.text = "Player vs Player";
                break;
        }
    }

    }

