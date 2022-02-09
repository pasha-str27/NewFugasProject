using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] BallMoving ballMoving;
    [SerializeField] float maxBallSpeed = 15;

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
                CheckSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                CheckSwipe();
            }
        }
    }

    void CheckSwipe()
    {
        var moveDirection = fingerDown - fingerUp;

        if(moveDirection.y > 0)
            ballMoving.SetMovingParameters(moveDirection.normalized, Mathf.Min(moveDirection.magnitude, maxBallSpeed));
    }
}
