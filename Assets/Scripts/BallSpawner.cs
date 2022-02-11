using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> ballPrefabs;
    [SerializeField] Vector2 startBallPosition;

    GameObject currentBall;

    void Start()
    {
        GameObject ballToSpawn = ballPrefabs[0];

        if (PlayerPrefs.HasKey("ChosenBall"))
            ballToSpawn = ballPrefabs[PlayerPrefs.GetInt("ChosenBall")];

        currentBall = Instantiate(ballToSpawn, startBallPosition, Quaternion.identity);
    }

    public void UpdateBall(int ballId)
    {
        Destroy(currentBall);
        currentBall = Instantiate(ballPrefabs[ballId], startBallPosition, Quaternion.identity);
    }
}
