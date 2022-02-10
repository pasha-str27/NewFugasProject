using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] BallMoving ballMoving;
    [SerializeField] float maxBallSpeed = 15;
    [SerializeField] TrajectoryRenderer trajectory;

    public static bool isMovingBall = false;

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }


            if (touch.phase == TouchPhase.Moved)
            {
                fingerDown = touch.position;

                var moveDirection = fingerDown - fingerUp;

                if(!isMovingBall && moveDirection.y > 0)
                    trajectory.ShowTrajectory(transform.position, moveDirection, 1);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                trajectory.ClearTrajectory();
                CheckSwipe();
            }
        }
    }

    void CheckSwipe()
    {
        var moveDirection = fingerDown - fingerUp;

        if (moveDirection.y > 0)
        {
            isMovingBall = true; 
            ballMoving.SetMovingParameters(moveDirection.normalized, Mathf.Min(moveDirection.magnitude, maxBallSpeed));
        }        
    }
}
