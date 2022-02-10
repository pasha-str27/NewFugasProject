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

    float _timeToReachTarget;
    Transform _transform;
    float _t = 0;
    float _distanceToMove;
    Vector3 _direction;


    void Start()
    {
        _transform = transform.parent.parent;
        ChangeMovementParameters();
    }

    void ChangeMovementParameters()
    {
        float angle = Random.Range(minAngleToRotate, maxAngleToRotate);
        _direction = new Vector3(Mathf.Cos(Mathf.PI * angle / 180), Mathf.Sin(Mathf.PI * angle / 180)).normalized;

        _transform.LookAt(_direction + _transform.position, -Vector3.forward);
        _transform.rotation = new Quaternion(0, 0, _transform.rotation.z, _transform.rotation.w);
    }


    private void FixedUpdate()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + _direction, Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.Instance().AddScore(1);
            Destroy(gameObject);
        }
    }
}
