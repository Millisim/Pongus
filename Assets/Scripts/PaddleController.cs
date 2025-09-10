using System;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Rigidbody2D rb;
    public int playerId;
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // We'd use a command like this if we didn't have a public Rigidbody2D
        // rb = gameObject.GetComponent<Rigidbody2D>();
        float axisValue = GetAxisValue();
        MovePaddle(axisValue);
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
