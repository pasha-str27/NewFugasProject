using System.Collections;
using UnityEngine;

public class BallMoving : MonoBehaviour
{
    [SerializeField] float speedCoeficient = 0.01f;
    [SerializeField] float maxFlyingTime = 8f;
    private Transform ballTransform;
    private Rigidbody rigidbody;
    [SerializeField] float minSpeed = 1;

    Vector2 startPosition;
    bool killedStickman;
    bool resetedBall;

    float flyingTime;

    Coroutine flyingCoroutine;
    BallSpawner spawner;

    public void SetKilledStickman(bool value) => killedStickman = value;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("BallSpawner")?.GetComponent<BallSpawner>();
        Time.timeScale = 1;

        resetedBall = true;
        killedStickman = false;

        ballTransform = gameObject.transform;
        startPosition = ballTransform.position;

        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rigidbody.velocity.magnitude < minSpeed && !SwipeController.isMovingBall && !resetedBall)
            ResetBall();
    }

    public void SetMovingParameters(Vector2 direction, float speed)
    {
        if (rigidbody.velocity.magnitude > 0)
            return;


        rigidbody.velocity = direction * speed * speedCoeficient;
        resetedBall = false;

        flyingTime = 0;

        flyingCoroutine = StartCoroutine(FindFlyingTime());
    }

    IEnumerator FindFlyingTime()
    {
        while (flyingTime < maxFlyingTime) 
        {
            flyingTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        ResetBall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DownWall") && ballTransform.position.y > other.gameObject.transform.position.y)
        {
            ResetBall();
            return;
        }
    }

    void ResetBall()
    {
        if (flyingCoroutine != null)
        {
            StopCoroutine(flyingCoroutine);
            flyingCoroutine = null;
        }

        ballTransform.position = startPosition;

        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = Vector2.zero;

        SwipeController.isMovingBall = false;

        if (!killedStickman)
            GameManager.Instance().MinusLife();

        spawner.UpdateBall();

        resetedBall = true;
        killedStickman = false;
    }
}
