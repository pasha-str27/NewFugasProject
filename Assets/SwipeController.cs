using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] BallMoving ballMoving;

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
                checkSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        var moveDirection = fingerDown - fingerUp;

        if(moveDirection.y > 0)
        {
            ballMoving.SetSpeed(moveDirection.magnitude);
            ballMoving.SetMovingDirection(moveDirection.normalized);
        }
    }
}
