using UnityEngine;
using UnityEngine.InputSystem;

public class Ball_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameManager gameManager;
    public float ballSpeed = 4f;
    public float delay = 4f;
    public float maxInitialAngle = 0.67f;
    private float startX = 0f;
    public float startY = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Serve", delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Serve()
    {
        // Find the rigidbody component
        // rb = GetComponent<Rigidbody2D>();

        // Generate random (x,y) vector2

        Vector2 direction = Vector2.left;
        if (Random.value > 0.5)
        {
            direction = Vector2.right;
        }
        direction.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        //rb.AddForce(direction);
        rb.linearVelocity = direction * ballSpeed;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag != "funnyWall")
        {
            gameManager.SetScores(collision.tag);

            ballSpeed = ballSpeed + 0.2f;
            ResetBall();
            Invoke("Serve", 1);
        }
        else if (collision.tag == "funnyWall")
        {
            ballSpeed = ballSpeed + 0.2f;
        }
    }

    private void ResetBall()
    {
        Vector2 direction = Vector2.zero;
        direction.y = Random.Range(-startY, startY);
        rb.transform.position = direction;
        rb.linearVelocity = Vector2.zero;
    }
}
