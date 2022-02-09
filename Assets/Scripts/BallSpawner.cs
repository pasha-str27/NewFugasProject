using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> ballPrefabs;
    [SerializeField] Vector2 startBallPosition;

    void Start()
    {
        GameObject ballToSpawn = ballPrefabs[0];

        if (PlayerPrefs.HasKey("ChosenBall"))
            ballToSpawn = ballPrefabs[PlayerPrefs.GetInt("ChosenBall")];

        Instantiate(ballToSpawn, startBallPosition, Quaternion.identity);

        Destroy(gameObject);
    }
}
