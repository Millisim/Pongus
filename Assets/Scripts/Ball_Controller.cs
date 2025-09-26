using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public float ballSpeed = 4f;
    public float maxInitialAngle = 0.67f;
    public float startY = 4f;
    public float baseBallSpeed;

    private void Awake()
    {
       baseBallSpeed = ballSpeed; // Remember the starting speed
    }

    private void OnEnable()
    {
        // Subscribe to GameManager's score updates (Observer pattern)
        //if (GameManager.Instance != null)
            GameManager.Instance.OnScore += OnScoreReached;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        if (GameManager.Instance != null)
            GameManager.Instance.OnScore -= OnScoreReached;
    }

    // Called when a score is made; resets the ball
    private void OnScoreReached(int score1, int score2)
    {
        ResetBall();
        Serve();
    }

    public void Serve()
    {
        Vector2 direction = UnityEngine.Random.value > 0.5f ? Vector2.right : Vector2.left;
        direction.y = UnityEngine.Random.Range(-maxInitialAngle, maxInitialAngle);
        rb.linearVelocity = direction * ballSpeed;
    }

    public void ResetBall()
    {
        Vector2 position = Vector2.zero;
        position.y = UnityEngine.Random.Range(-startY, startY);
        rb.transform.position = position;
        rb.linearVelocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Speed up by 0.2f no matter what it hits
        IncreaseBallSpeed(0.2f);

        // Extra speed boost if it's a paddle
        PaddleController paddle = collision.collider.GetComponent<PaddleController>();
        if (paddle != null)
        {
            rb.linearVelocity *= 1.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        // Ball speeds up when passing through ANY trigger
        IncreaseBallSpeed(0.2f);

        if (collision.CompareTag("L-Zone"))
            GameManager.Instance.SetScores("L-Zone"); // Right player scores

        else if (collision.CompareTag("R-Zone"))
            GameManager.Instance.SetScores("R-Zone"); // Left player scores
    }

    private void IncreaseBallSpeed(float amount)
    {
        ballSpeed += amount;
        rb.linearVelocity = rb.linearVelocity.normalized * ballSpeed;
    }
}
