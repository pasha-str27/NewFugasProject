using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoving : MonoBehaviour
{
    [SerializeField] float speedCoeficient = 0.01f;
    private Transform ballTransform;
    private Rigidbody rigidbody;
    [SerializeField] float minSpeed = 1;

    Vector2 startPosition;

    void Start()
    {
        GameManager.Instance().Reset();
        ballTransform = gameObject.transform;
        startPosition = ballTransform.position;

        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rigidbody.velocity.magnitude < minSpeed)
            ResetBall();
    }

    public void SetMovingParameters(Vector2 direction, float speed)
    {
        if (rigidbody.velocity.magnitude > 0)
            return;

        rigidbody.velocity = direction * speed * speedCoeficient;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("DownWall"))
        {
            ResetBall();
            return;
        }
    }

    void ResetBall()
    {
        ballTransform.position = startPosition;

        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = Vector2.zero;

        SwipeController.isMovingBall = false;

    }
}
