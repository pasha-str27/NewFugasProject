using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoving : MonoBehaviour
{
    [SerializeField] float speedCoeficient = 0.01f;
    private Vector2 movingDirection = Vector2.zero;
    private float speed = 0;
    private Transform ballTransform;

    Vector2 startPosition;

    bool canProcessSwipe = true;

    void Start()
    {
        ballTransform = gameObject.transform;
        startPosition = ballTransform.position;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;

        UpdateSwipeProcessing();
    }

    private void UpdateSwipeProcessing()
    {
        canProcessSwipe = !(speed != 0 && movingDirection.magnitude > 0);
    }

    public void SetMovingDirection(Vector2 direction)
    {
        movingDirection = direction;

        UpdateSwipeProcessing();
    }

    void FixedUpdate()
    {
        ballTransform.Translate(movingDirection * speed * Time.deltaTime * speedCoeficient);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RightWall") || collision.gameObject.CompareTag("LeftWall"))
        {
            movingDirection = new Vector2(-movingDirection.x, movingDirection.y);
            return;
        }

        if (collision.gameObject.CompareTag("TopWall"))
        {
            movingDirection = new Vector2(movingDirection.x, -movingDirection.y);
            return;
        }

        if (collision.gameObject.CompareTag("DownWall"))
        {
            movingDirection = Vector2.zero;
            speed = 0;
            ballTransform.position = startPosition;
            canProcessSwipe = true;

            return;
        }
    }
}
