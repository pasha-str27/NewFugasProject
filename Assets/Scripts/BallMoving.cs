using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoving : MonoBehaviour
{
    [SerializeField] float speedCoeficient = 0.01f;
    private Transform ballTransform;
    private Rigidbody rigidbody;

    Vector2 startPosition;

    void Start()
    {
        GameManager.Instance().Reset();
        ballTransform = gameObject.transform;
        startPosition = ballTransform.position;

        rigidbody = GetComponent<Rigidbody>();
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
            ballTransform.position = startPosition;

            rigidbody.velocity = Vector2.zero;

            return;
        }
    }
}
