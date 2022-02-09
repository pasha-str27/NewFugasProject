using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanBehaviour : MonoBehaviour
{
    [SerializeField] float minTimeToMove = 1;
    [SerializeField] float maxTimeToMove = 2;
    [SerializeField] float minDistance = 0.5f;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] float minAngleToRotate = -180;
    [SerializeField] float maxAngleToRotate = 180;

    [SerializeField] int scoreCount = 1;


    void Start()
    {
        StickmanSpawner.stickmansCount += 1;

        ChangeMovementParameters();
    }

    void ChangeMovementParameters()
    {
        float DegToRad(float angle) => Mathf.PI * angle / 180;

        var _transform = transform;

        float angle = DegToRad(Random.Range(minAngleToRotate, maxAngleToRotate));

        Vector3 _direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        _transform.LookAt(_direction + _transform.position, -Vector3.forward);
        _transform.rotation = new Quaternion(0, 0, _transform.rotation.z, _transform.rotation.w);

        _transform.GetComponent<Rigidbody>().velocity = _direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance().AddScore(scoreCount);
            Destroy(gameObject);
            StickmanSpawner.stickmansCount -= 1;
        }
    }
}
