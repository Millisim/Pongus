using System;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Rigidbody2D rb;
    public int playerId;
    public float moveSpeed = 5f;
    public float cpuDelay = 1f;

    private Vector2 startPosition;

    public Ball_Controller ball;

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Decide if player 1 is a CPU or not
        if (playerId == 1 && GameManager.Instance.playMode == GameManager.PlayMode.CPUVsCPU)
        {
            Invoke("MoveCPU", cpuDelay);
        }
        else
        {
            // Moves the paddle based on player input
            float axisValue = GetAxisValue();
            MovePaddle(axisValue);
        }

        // Decide if player 2 is a CPU or not
        if (playerId == 2 && GameManager.Instance.playMode == GameManager.PlayMode.PlayerVsCPU || GameManager.Instance.playMode == GameManager.PlayMode.CPUVsCPU)
        {
            Invoke("MoveCPU", cpuDelay);
        }
        else
        {
            // Moves the paddle based on player input
            float axisValue = GetAxisValue();
            MovePaddle(axisValue);
        }
    }

    private void MoveCPU()
    {
        Vector2 ballPosition = ball.transform.position;
        transform.position = new Vector2(startPosition.x, ballPosition.y);
    }

    private void MovePaddle(float axisValue)
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = axisValue * moveSpeed;
        rb.linearVelocity = velocity;
    }

    private float GetAxisValue()
    {
        float axisValue = 0f;
        if (playerId == 1)
            axisValue = Input.GetAxis("PaddlePlayer1");
        if (playerId == 2)
            axisValue = Input.GetAxis("PaddlePlayer2");
        return axisValue;
    }
}
