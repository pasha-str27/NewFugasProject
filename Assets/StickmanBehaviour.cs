using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.Instance().AddScore(1);
            Destroy(gameObject);
        }
    }
}
