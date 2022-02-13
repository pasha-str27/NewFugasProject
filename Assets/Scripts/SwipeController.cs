using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] BallMoving ballMoving;
    [SerializeField] float maxBallSpeed = 15;
    [SerializeField] TrajectoryRenderer trajectory;
    [SerializeField] float swipeThershold;

    public static bool isMovingBall = false;

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    void Update()
    {
        if (GameManager.Instance().IsPaused())
            return;

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

        if (moveDirection.y > 0 && moveDirection.magnitude > swipeThershold)
        {
            isMovingBall = true; 
            ballMoving.SetMovingParameters(moveDirection.normalized, Mathf.Min(moveDirection.magnitude, maxBallSpeed));
        }        
    }
}
